using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using ch.ph.FileSystem;

namespace ch.ph.FileManager
{
    public class BackgroundJob
    {
        private delegate void FVoid();

        private readonly Form             _owner;
        private readonly BackgroundWindow _wnd;
        private volatile bool             _cancel;

        public BackgroundJob(Form owner)
        {
            _owner  = owner;
            _wnd    = new BackgroundWindow(new EventHandler(OnCancel));
            _cancel = false;
        }

        public void Start(List<IElement> copysrc, IDirectory copydst, List<IElement> del, string title)
        {
            _wnd.Text = title;
            _wnd.Show(_owner);

            new Thread( (ThreadStart) delegate { Work(copysrc, copydst, del); } ).Start();
        }

        private void OnCancel(object sender, EventArgs e) {
            _cancel = true;
        }

        private void SingleCopy(IFile srcfile, IDirectory dstdirpar) 
        {
            IElement dstelem = dstdirpar.GetChild(srcfile.Name); // TODO: try catch

            if(dstelem != null) {
                throw new Exception(dstelem.FullName + " already exists.");
            }

            _wnd.Invoke( (FVoid) delegate { _wnd.Line1 = "Copying " + srcfile.FullName + " ..."; }  );

            bool retry;
            do {
                retry = false;
                try {
                    using(Stream srcstm = srcfile.Open(true, false)) {
                        using(Stream dststm = dstdirpar.CreateFileAndOpen(srcfile.Name, srcfile.Size, srcfile.ContentType, false, true) /* dstfile.Open(false, true) */) {

                            long   pos = 0;
                            long   len = srcstm.Length;
                            byte[] buf = new byte[4096]; // Setting a lower value seems to help against timeouts at Amazon S3 (before: 65536)

                            while(len > 0) {
                                int now = (len > buf.Length) ? buf.Length : (int) len;

                                now = srcstm.Read (buf, 0, now);
                                      dststm.Write(buf, 0, now);
                                      dststm.Flush();

                                len -= now;
                                pos += now;

                                _wnd.Invoke( (FVoid) delegate { _wnd.Line2 = ((pos + 1023) >> 10) + " KB copied."; }  );

                                if(_cancel) break;
                            }
                        }
                    }
                } catch(Exception ex) {
                    switch(MessageBox.Show("Failed to copy '" + srcfile.FullName + "' to '" + dstdirpar.FullName + "'.\n" + ex.Message + "\n\nDo you want to retry?\nSelect 'Cancel' to abort.", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error)) {
                        case DialogResult.Yes:    retry   = true;  break;
                        case DialogResult.No:     retry   = false; break;
                        case DialogResult.Cancel: _cancel = true;  break; 
                    }
                    try {
                        dstelem = dstdirpar.GetChild(srcfile.Name);
                        if(dstelem != null) dstelem.Delete();
                    } catch { }
                }
            } while(retry && !_cancel);

            _wnd.Invoke( (FVoid) delegate { _wnd.Line2 = null; }  );
        }

        private void SingleDelete(IElement elem)
        {
            _wnd.Invoke( (FVoid) delegate { _wnd.Line1 = "Deleting " + elem.FullName + " ..."; }  );

            bool retry = false;
            do {
                try {
                    elem.Delete();

                } catch(Exception ex) {
                    switch(MessageBox.Show("Failed to delete '" + elem.FullName + ".\n" + ex.Message + "\n\nDo you want to retry?\nSelect 'Cancel' to abort.", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error)) {
                        case DialogResult.Yes:    retry   = true;  break;
                        case DialogResult.No:     retry   = false; break;
                        case DialogResult.Cancel: _cancel = true;  break; 
                    }
                }
            } while(retry && !_cancel);
        }

        private void RecursiveCopy(IElement srcelem, IDirectory dstdirpar)
        {
            if(srcelem.IsDirectory) {

                IDirectory srcdir  = (IDirectory) srcelem;
                IElement   dstelem = dstdirpar.GetChild(srcdir.Name); // TODO: try catch

                if(dstelem != null) {
                    throw new Exception(dstelem.FullName + " already exists.");
                }

                IDirectory dstdir = dstdirpar.CreateDirectory(srcdir.Name); // TODO: try catch

                foreach(IDirectory srcsub in srcdir.GetDirectories()) { // TODO: try catch 
                    if(!_cancel) RecursiveCopy(srcsub, dstdir);
                }

                foreach(IFile srcsub in srcdir.GetFiles()) { // TODO: try catch
                    if(!_cancel) RecursiveCopy(srcsub, dstdir);
                }

            } else {

                SingleCopy((IFile) srcelem, dstdirpar);
            }
        }

        private void RecursiveDelete(IElement elem)
        {
            if(elem.IsDirectory) {

                IDirectory dir = (IDirectory) elem;

                foreach(IElement subelem in dir.GetDirectories()) {
                    if(!_cancel) RecursiveDelete(subelem);
                }

                foreach(IElement subelem in dir.GetFiles()) {
                    if(!_cancel) RecursiveDelete(subelem);
                }
            }

            if(!_cancel) {
                SingleDelete(elem);
            }
        }

        private void Work(List<IElement> copysrc, IDirectory copydst, List<IElement> del)
        {
            if(copysrc != null && copydst != null) {
                foreach(IElement srcelem in copysrc) {
                    if(!_cancel) RecursiveCopy(srcelem, copydst);
                }
            }

            if(del != null) {
                foreach(IElement elem in del) {
                    if(!_cancel) RecursiveDelete(elem);
                }
            }

            _wnd.Invoke( (FVoid) delegate { _wnd.Close(); }  );
        }
    }
}