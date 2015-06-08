using System;
using System.Collections.Generic;
using System.IO;
using ch.ph.FileSystem;

namespace ch.ph.FileSystem.LocalFileSystem {

    internal class DirectoryImpl : IDirectory {

        // *** Private Attributes *******************************************

        private const string BADCHARS = "/\\:*?\"<>|";

        private readonly DirectoryImpl _parent;
        private readonly string        _fullname;
        private readonly string        _localname;

        // *** Private Methods **********************************************

        private static void CheckName(string localname) 
        {
            if(localname == null || localname.Length == 0 || localname.IndexOfAny(BADCHARS.ToCharArray()) != -1) {
                throw new Exception("'" + localname + "' is not a valid file or directory name. Names must not be empty and must not contain the following characters: " + BADCHARS);
            }
        }

        private string DoGetFullName(string localname)
        {
            if(_fullname.EndsWith("\\")) {
                return _fullname + localname;
            } else {
                return _fullname + "\\" + localname;
            }
        }

        private string DoGetLocalName(string fullname) 
        {
            if(fullname.Length <= _fullname.Length) {
                return "???";
            } else if(fullname[_fullname.Length] == '\\') {
                return fullname.Substring(_fullname.Length + 1);
            } else {
                return fullname.Substring(_fullname.Length);
            }
        }

        private IDictionary<string, IElement> DoGetChildren() 
        {
            IDictionary<string, IElement> children = new Dictionary<string, IElement>();

            foreach(string fullname in Directory.GetDirectories(_fullname)) {
                string localname = DoGetLocalName(fullname);
                children.Add(localname, new DirectoryImpl(this, fullname, localname));
            }

            foreach(string fullname in Directory.GetFiles(_fullname)) {
                string localname = DoGetLocalName(fullname);
                children.Add(localname, new FileImpl(this, fullname, localname));
            }

            return children;
        }

        // *** Internal Methods *********************************************

        internal DirectoryImpl(DirectoryImpl parent, string fullname, string localname) {
            _parent    = parent;
            _fullname  = fullname;
            _localname = localname;
        }

        // *** IDirectory and IElement **************************************

        string IElement.Name {
            get { return _localname;  }
        }

        string IElement.FullName {
            get { return _fullname;  }
        }

        IDirectory IElement.Parent {
            get { return _parent; }
        }

        bool IElement.IsDirectory {
            get { return true; }
        }

        bool IElement.IsFile {
            get { return false; }
        }

        void IElement.Delete() {
            Directory.Delete(_fullname, false);
        }

        IList<IDirectory> IDirectory.GetDirectories() {
            IList<IDirectory> res = new List<IDirectory>();
            foreach(IElement child in DoGetChildren().Values) {
                if(child.IsDirectory) {
                    res.Add((IDirectory) child);
                }
            }
            return res;
        }

        IList<IFile> IDirectory.GetFiles() {
            IList<IFile> res = new List<IFile>();
            foreach(IElement child in DoGetChildren().Values) {
                if(child.IsFile) {
                    res.Add((IFile) child);
                }
            }
            return res;
        }

        IElement IDirectory.GetChild(string name) {
            IElement res;
            DoGetChildren().TryGetValue(name, out res); // TODO: optimize performance
            return res;
        }

        IDirectory IDirectory.CreateDirectory(string name) {
            CheckName(name);
            string fullname = DoGetFullName(name);
            Directory.CreateDirectory(fullname);
            return new DirectoryImpl(this, fullname, name);
        }

        Stream IDirectory.CreateFileAndOpen(string name, long? size, string contentType, bool read, bool write) 
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

            CheckName(name);
            string fullname = DoGetFullName(name);
            return new FileStream(fullname, FileMode.CreateNew, fa);
        }
    }
}