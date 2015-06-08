using System;
using System.Collections.Generic;
using System.IO;
using ch.ph.AmazonWS;
using ch.ph.FileSystem;

namespace ch.ph.FileSystem.AmazonS3 {

    internal class FileImpl : IFile {

        // *** Private Attributes *******************************************

        private readonly FileSystem      _owner;
        private readonly DirectoryImpl   _parent;
        private readonly string          _fullname;
        private readonly string          _localname;
        private readonly long            _size;
        private readonly string          _contentType;
       
        // *** Internal Methods *********************************************

        internal FileImpl(FileSystem owner, DirectoryImpl parent, string fullname, string localname, long size, string contentType) {
            _owner       = owner;
            _parent      = parent;
            _fullname    = fullname;
            _localname   = localname;
            _size        = size;
            _contentType = contentType;
        }

        // *** IFile and IElement *******************************************

        string IElement.Name {
            get { return _localname; }
        }

        string IElement.FullName {
            get { return _owner.Bucket + "/" + _fullname;  }
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
            _owner.Service.DeleteObject(_owner.Bucket, _fullname);
        }

        long IFile.Size {
            get { return _size; }
        }

        string IFile.ContentType { 
            get { return _contentType; }
        }

        Stream IFile.Open(bool read, bool write) 
        {
            if(read && write) {
                throw new ArgumentException();
            } else if(read) {
                return _owner.Service.GetObjectStream(_owner.Bucket, _fullname);
            } else if(write) {
                throw new ArgumentException();
            } else {
                throw new ArgumentException();
            }
        }
    }
}