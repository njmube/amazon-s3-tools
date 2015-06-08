using System;
using System.Collections.Generic;
using System.IO;
using ch.ph.FileSystem;

namespace ch.ph.FileSystem.LocalFileSystem {

    internal class FileImpl : IFile {

        // *** Private Attributes *******************************************

        private readonly DirectoryImpl _parent;
        private readonly string        _fullname;
        private readonly string        _localname;
        
        // *** Private Methods **********************************************

        private long DoGetSize() {
            using(Stream stm = new FileStream(_fullname, FileMode.Open, FileAccess.Read)) {
                return stm.Length;
            }
        }

        // *** Internal Methods *********************************************

        internal FileImpl(DirectoryImpl parent, string fullname, string localname) {
            _parent    = parent;
            _fullname  = fullname;
            _localname = localname;
        }

        // *** IFile and IElement *******************************************

        string IElement.Name {
            get { return _localname; }
        }

        string IElement.FullName {
            get { return _fullname;  }
        }

        IDirectory IElement.Parent {
            get { return _parent; }
        }

        bool IElement.IsDirectory {
            get { return false; }
        }

        bool IElement.IsFile {
            get { return true; }
        }

        void IElement.Delete() {
            File.Delete(_fullname);
        }

        long IFile.Size {
            get { return DoGetSize(); }
        }

        string IFile.ContentType { 
            get { return ContentTypeDectector.Instance.GetContentTypeForFile(_localname); } 
        }

        Stream IFile.Open(bool read, bool write)
        {
            FileAccess fa;

            if(read && write) {
                fa = FileAccess.ReadWrite;
            } else if(read) {
                fa = FileAccess.Read;
            } else if(write) {
                fa = FileAccess.ReadWrite;
            } else {
                throw new ArgumentException();
            }

            return new FileStream(_fullname, FileMode.Open, fa);
        }
    }
}