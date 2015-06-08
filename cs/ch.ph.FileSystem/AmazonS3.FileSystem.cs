using System;
using ch.ph.AmazonWS;
using ch.ph.FileSystem;

namespace ch.ph.FileSystem.AmazonS3 {

    public class FileSystem : IFileSystem {

        // *** Private Attributes *******************************************

        private readonly IStorageService _svc;
        private readonly string          _bucket;
        private readonly AWSGrant        _acl;
        private readonly DirectoryImpl   _root;

        // *** Internal Methods *********************************************

        internal IStorageService Service { get { return _svc; } }

        internal string Bucket { get { return _bucket; } }

        internal AWSGrant Acl { get { return _acl; } }

        // *** Public Methods ***********************************************

        public FileSystem(string svc, string accessKeyId, string secretAccessKey, string bucket, string acl) 
        {
            if(svc == "REST") {
                _svc = new RestStorageService();
            } else if(svc == "SOAP") {
                _svc = new SoapStorageService();
            } else {
                throw new ArgumentException();
            }

            if(acl == "PrivateOnly") {
                _acl = AWSGrant.PrivateOnly;
            } else if(acl == "PublicRead") {
                _acl = AWSGrant.PublicRead;
            } else if(acl == "PublicWrite") {
                _acl = AWSGrant.PublicWrite;
            } else {
                throw new ArgumentException();
            }

            _svc.AccessKeyId     = accessKeyId;
            _svc.SecretAccessKey = secretAccessKey;

            _bucket = bucket;
            _root   = new DirectoryImpl(this, null, "", _bucket);
        }

        // *** IFileSystem **************************************************

        string IFileSystem.TypeName { 
            get { return "Amazon S3"; }
        }

        IDirectory IFileSystem.RootDirectory {
            get { return _root; }
        }
    }

}