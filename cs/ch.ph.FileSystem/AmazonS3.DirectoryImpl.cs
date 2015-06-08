using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ch.ph.AmazonWS;
using ch.ph.FileSystem;

namespace ch.ph.FileSystem.AmazonS3 {

    internal class DirectoryImpl : IDirectory {

        // *** Private Attributes *******************************************

        private const string BADCHARS = "/\\";
        private const string DIRFILE  = ".directory";

        private readonly FileSystem      _owner;
        private readonly DirectoryImpl   _parent;
        private readonly string          _fullname;
        private readonly string          _localname;

        // *** Private Methods **********************************************

        private static void CheckName(string localname) 
        {
            if(localname == null || localname.Length == 0 || localname == "." || localname == ".." || localname == DIRFILE || localname.IndexOfAny(BADCHARS.ToCharArray()) != -1) {
                throw new Exception("'" + localname + "' is not a valid file or directory name. Names must not be empty and must not contain the following characters: " + BADCHARS);
            }
        }

        private string DoGetFullName(string localname)
        {
            if(_fullname == "" || _fullname.EndsWith("/")) {
                return _fullname + localname;
            } else {
                return _fullname + "/" + localname;
            }
        }

        private string DoGetLocalName(string fullname) 
        {
            if(fullname.Length <= _fullname.Length) {
                return "???";
            } else if(fullname[_fullname.Length] == '/') {
                return fullname.Substring(_fullname.Length + 1);
            } else {
                return fullname.Substring(_fullname.Length);
            }
        }

        private static string GetMetaValue(AWSMetadataEntry[] meta, string metakey)
        {
            if(meta != null) {
                foreach(AWSMetadataEntry entry in meta) {
                    if(entry.MetaKey.ToLower() == metakey.ToLower()) {
                        return entry.MetaValue;
                    }
                }
            }
            return null;
        }

        private IDictionary<string, IElement> DoGetChildren() 
        {
            IDictionary<string, IElement> children = new Dictionary<string, IElement>();

            AWSBucket bucket = _owner.Service.ListBucket(_owner.Bucket, _fullname, null, null, "/");

            foreach(string prefix in bucket.ObjectPrefixes) {
                string fullname  = prefix.Substring(0, prefix.Length - 1);
                string localname = DoGetLocalName(fullname);
                children.Add(prefix, new DirectoryImpl(_owner, this, fullname + "/", localname));
            }

            foreach(AWSObject child in bucket.Objects) {
                string fullname    = child.Key;
                string localname   = DoGetLocalName(fullname);
                long   size        = child.Size;
                string contentType = GetMetaValue(child.Metadata, "Content-Type"); // TODO child.Metadata is always null!
                if(localname == DIRFILE) continue;
                children.Add(child.Key, new FileImpl(_owner, this, fullname, localname, size, contentType));
            }

            return children;
        }

        // *** Internal Methods *********************************************

        internal DirectoryImpl(FileSystem owner, DirectoryImpl parent, string fullname, string localname) {
            _owner     = owner;
            _parent    = parent;
            _fullname  = fullname;
            _localname = localname;
        }

        // *** IDirectory and IElement **************************************

        string IElement.Name {
            get { return _localname;  }
        }

        string IElement.FullName {
            get { return _owner.Bucket + "/" + _fullname;  }
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
            _owner.Service.DeleteObject(_owner.Bucket, _fullname + DIRFILE);
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

        IDirectory IDirectory.CreateDirectory(string name) 
        {
            AWSMetadataEntry[] meta = new AWSMetadataEntry[1];
            meta[0] = new AWSMetadataEntry();
            meta[0].MetaKey   = "content-type";
            meta[0].MetaValue = "text/plain";

            byte[] data = Encoding.UTF8.GetBytes("This is a directory.");

            CheckName(name);
            string fullname = DoGetFullName(name);
            _owner.Service.CreateObject(_owner.Bucket, fullname + "/" + DIRFILE, meta, data, _owner.Acl);
            return new DirectoryImpl(_owner, this, fullname, name);
        }

        Stream IDirectory.CreateFileAndOpen(string name, long? size, string contentType, bool read, bool write)
        {
            if(read || !write) {
                throw new ArgumentException();
            }

            AWSMetadataEntry[] meta = null;
            if(contentType != null) {
                meta = new AWSMetadataEntry[1];
                meta[0] = new AWSMetadataEntry();
                meta[0].MetaKey   = "content-type";
                meta[0].MetaValue = contentType;
            }

            CheckName(name);
            string fullname = DoGetFullName(name);
            return _owner.Service.CreateObjectStream(_owner.Bucket, fullname, size, meta, _owner.Acl);
        }
    }
}