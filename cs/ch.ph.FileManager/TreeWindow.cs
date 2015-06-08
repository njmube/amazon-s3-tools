using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ch.ph.FileSystem;

namespace ch.ph.FileManager
{
    public partial class TreeWindow : Form
    {
        private readonly Clipboard   _clip;
        private readonly IFileSystem _fs;
        private          IDirectory  _cdir;

        private static void InitNode(TreeNode node, IDirectory dir)
        {
            node.Text               = dir.Name;
            node.Tag                = dir;
            node.ImageIndex         = 1;
            node.SelectedImageIndex = 1;

            node.Nodes.Add("x", "dummy", 0);
        }

        public TreeWindow(Clipboard clip, IFileSystem fs) {
            _clip = clip;
            _fs   = fs;

            InitializeComponent();

            Text = Text + " - " + fs.TypeName;

            _tree.BeforeExpand  += new TreeViewCancelEventHandler(this._tree_BeforeExpand);
            _tree.AfterCollapse += new TreeViewEventHandler(this._tree_AfterCollapse);

            IDirectory dir  = fs.RootDirectory;
            TreeNode   node = new TreeNode();
            InitNode(node, dir);
            _tree.Nodes.Add(node);

            _list.Columns.Add("Name", 300);
            _list.Columns.Add("Size", 100);
            _list.Columns.Add("Type", 100);

            _clip.ClipboardContentChanged += new EventHandler(this.UpdateButtons); // TODO remove when closed
            _list.SelectedIndexChanged    += new EventHandler(this.UpdateButtons);
            _btCut.      Click += new EventHandler(this.OnCut);
            _btCopy.     Click += new EventHandler(this.OnCopy);
            _btPaste.    Click += new EventHandler(this.OnPaste);
            _btDelete.   Click += new EventHandler(this.OnDelete);
            _btCreateDir.Click += new EventHandler(this.OnCreateDir);

            UpdateButtons(null, null);
        }

        private void _tree_AfterSelect(object sender, TreeViewEventArgs e) {
            TreeNode   node = e.Node; 
            IDirectory dir  = (IDirectory) node.Tag;

            _cdir = dir;
            _address.Text = dir.FullName;
            _list.Items.Clear();

            int  dircnt  = 0;
            int  filecnt = 0;
            long totsize = 0;

            try {
                foreach(IDirectory subdir in dir.GetDirectories()) {
                    ListViewItem subitem = new ListViewItem(subdir.Name, 1);
                    subitem.Tag = subdir;
                    _list.Items.Add(subitem);
                    dircnt++;
                }

                foreach(IFile subfile in dir.GetFiles()) {
                    ListViewItem subitem = new ListViewItem(subfile.Name, 2);
                    long         subsize = subfile.Size;
                    subitem.Tag = subfile;
                    subitem.SubItems.Add(((subsize + 1023) >> 10) + " KB");
                    subitem.SubItems.Add(subfile.ContentType);
                    _list.Items.Add(subitem);
                    filecnt++;
                    totsize += subsize;
                }
            } catch(Exception ex) {
                MessageBox.Show("Failed to read files and subdirectories.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _status.Text = string.Format("{0}: {1} directories, {2} files, {3} KB", dir.FullName, dircnt, filecnt, ((totsize + 1023) >> 10));

            UpdateButtons(null, null);
        }

        private void _tree_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            TreeNode   node = e.Node;
            IDirectory dir  = (IDirectory) node.Tag;

            node.Nodes.Clear();

            try {
                foreach(IDirectory subdir in dir.GetDirectories()) {
                    TreeNode subnode = new TreeNode();
                    InitNode(subnode, subdir);
                    node.Nodes.Add(subnode);
                }
            } catch(Exception ex) {
                MessageBox.Show("Failed to read subdirectories.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _tree_AfterCollapse(object sender, TreeViewEventArgs e) {
            TreeNode   node = e.Node;
            IDirectory dir  = (IDirectory) node.Tag;

            node.Nodes.Clear();

            InitNode(node, dir);
        }

        private void UpdateButtons(object sender, EventArgs e)
        {
            int clipcnt = _clip.GetContentCount();
            int listcnt = _list.SelectedItems.Count;

            _btCut.      Enabled = (listcnt > 0);
            _btCopy.     Enabled = (listcnt > 0);
            _btPaste.    Enabled = (clipcnt > 0) /* && (_cdir != null)*/;
            _btDelete.   Enabled = (listcnt > 0);
            _btCreateDir.Enabled = (_cdir != null);
        }

        private void OnCut(object sender, EventArgs e)
        {
            List<IElement> elems = new List<IElement>();
            foreach(ListViewItem item in _list.SelectedItems) {
                IElement elem = (IElement) item.Tag;
                elems.Add(elem);
            }
            
            _clip.SetContent(elems, true);
        }

        private void OnCopy(object sender, EventArgs e)
        {
            List<IElement> elems = new List<IElement>();
            foreach(ListViewItem item in _list.SelectedItems) {
                IElement elem = (IElement) item.Tag;
                elems.Add(elem);
            }
            
            _clip.SetContent(elems, false);
        }

        private void OnPaste(object sender, EventArgs e)
        {
            List<IElement> elems = _clip.GetContent();

            if(_cdir != null && elems != null && elems.Count > 0) {
                bool cut = _clip.GetContentCut();
                if(cut) _clip.SetContent(null, false);
                
                new BackgroundJob(this).Start(elems, _cdir, cut ? elems : null, cut ? "Moving..." : "Copying...");
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if(_list.SelectedItems.Count >= 1) {
                if(MessageBox.Show(string.Format("Do you really want to delete {0} item(s)?", _list.SelectedItems.Count), "Confirm Deletion", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {
                    
                    List<IElement> elems = new List<IElement>();
                    foreach(ListViewItem item in _list.SelectedItems) {
                        IElement elem = (IElement) item.Tag;
                        elems.Add(elem);
                    }

                    new BackgroundJob(this).Start(null, null, elems, "Deleting...");
                }
            }
        }

        private void OnCreateDir(object sender, EventArgs e)
        {
            if(_cdir != null) {
                new CreateDirectory(_cdir).ShowDialog();
            }
        }
    }
}