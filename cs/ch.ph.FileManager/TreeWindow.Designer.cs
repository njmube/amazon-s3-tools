namespace ch.ph.FileManager
{
    partial class TreeWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeWindow));
            this._tree = new System.Windows.Forms.TreeView();
            this._icons = new System.Windows.Forms.ImageList(this.components);
            this._address = new System.Windows.Forms.TextBox();
            this._list = new System.Windows.Forms.ListView();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._btCut = new System.Windows.Forms.ToolStripButton();
            this._btCopy = new System.Windows.Forms.ToolStripButton();
            this._btPaste = new System.Windows.Forms.ToolStripButton();
            this._btDelete = new System.Windows.Forms.ToolStripButton();
            this._btCreateDir = new System.Windows.Forms.ToolStripButton();
            this._status = new System.Windows.Forms.TextBox();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tree
            // 
            this._tree.ImageIndex = 0;
            this._tree.ImageList = this._icons;
            this._tree.Location = new System.Drawing.Point(12, 54);
            this._tree.Name = "_tree";
            this._tree.SelectedImageIndex = 0;
            this._tree.Size = new System.Drawing.Size(273, 499);
            this._tree.TabIndex = 0;
            this._tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._tree_AfterSelect);
            // 
            // _icons
            // 
            this._icons.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("_icons.ImageStream")));
            this._icons.TransparentColor = System.Drawing.Color.Transparent;
            this._icons.Images.SetKeyName(0, "Error16x16.bmp");
            this._icons.Images.SetKeyName(1, "Directory16x16.bmp");
            this._icons.Images.SetKeyName(2, "File16x16.bmp");
            // 
            // _address
            // 
            this._address.Location = new System.Drawing.Point(12, 28);
            this._address.Name = "_address";
            this._address.ReadOnly = true;
            this._address.Size = new System.Drawing.Size(817, 20);
            this._address.TabIndex = 1;
            // 
            // _list
            // 
            this._list.Location = new System.Drawing.Point(291, 54);
            this._list.Name = "_list";
            this._list.Size = new System.Drawing.Size(538, 499);
            this._list.SmallImageList = this._icons;
            this._list.TabIndex = 2;
            this._list.UseCompatibleStateImageBehavior = false;
            this._list.View = System.Windows.Forms.View.Details;
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btCut,
            this._btCopy,
            this._btPaste,
            this._btDelete,
            this._btCreateDir});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(841, 25);
            this._toolStrip.TabIndex = 3;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _btCut
            // 
            this._btCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btCut.Image = ((System.Drawing.Image) (resources.GetObject("_btCut.Image")));
            this._btCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btCut.Name = "_btCut";
            this._btCut.Size = new System.Drawing.Size(23, 22);
            this._btCut.Text = "Cut";
            // 
            // _btCopy
            // 
            this._btCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btCopy.Image = ((System.Drawing.Image) (resources.GetObject("_btCopy.Image")));
            this._btCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btCopy.Name = "_btCopy";
            this._btCopy.Size = new System.Drawing.Size(23, 22);
            this._btCopy.Text = "Copy";
            // 
            // _btPaste
            // 
            this._btPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btPaste.Image = ((System.Drawing.Image) (resources.GetObject("_btPaste.Image")));
            this._btPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btPaste.Name = "_btPaste";
            this._btPaste.Size = new System.Drawing.Size(23, 22);
            this._btPaste.Text = "Paste";
            // 
            // _btDelete
            // 
            this._btDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btDelete.Image = ((System.Drawing.Image) (resources.GetObject("_btDelete.Image")));
            this._btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btDelete.Name = "_btDelete";
            this._btDelete.Size = new System.Drawing.Size(23, 22);
            this._btDelete.Text = "Delete";
            // 
            // _btCreateDir
            // 
            this._btCreateDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btCreateDir.Image = ((System.Drawing.Image) (resources.GetObject("_btCreateDir.Image")));
            this._btCreateDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btCreateDir.Name = "_btCreateDir";
            this._btCreateDir.Size = new System.Drawing.Size(23, 22);
            this._btCreateDir.Text = "Create Directory";
            // 
            // _status
            // 
            this._status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._status.Location = new System.Drawing.Point(12, 559);
            this._status.Name = "_status";
            this._status.ReadOnly = true;
            this._status.Size = new System.Drawing.Size(817, 13);
            this._status.TabIndex = 4;
            // 
            // TreeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 585);
            this.Controls.Add(this._status);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._list);
            this.Controls.Add(this._address);
            this.Controls.Add(this._tree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TreeWindow";
            this.Text = "File Manager";
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView _tree;
        private System.Windows.Forms.TextBox _address;
        private System.Windows.Forms.ListView _list;
        private System.Windows.Forms.ImageList _icons;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _btCut;
        private System.Windows.Forms.ToolStripButton _btCopy;
        private System.Windows.Forms.ToolStripButton _btPaste;
        private System.Windows.Forms.ToolStripButton _btDelete;
        private System.Windows.Forms.ToolStripButton _btCreateDir;
        private System.Windows.Forms.TextBox _status;
    }
}