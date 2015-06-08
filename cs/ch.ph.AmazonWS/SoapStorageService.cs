using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using ch.ph.AmazonWS.com.amazonaws.s3.doc;

namespace ch.ph.AmazonWS
{
    public sealed class SoapStorageService : IStorageService
    {
        private static string SERVICE_NAME     = "AmazonS3";
        private static string TIMESTAMP_FORMAT = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";

        private AmazonS3 _service;
        private string   _accessKeyId;
        private string   _secretAccessKey;

        // *** 

        public SoapStorageService() {
            _service = new AmazonS3();
        }

        // *** IStorageService

        string IStorageService.AccessKeyId {
            set { _accessKeyId = value; }
        }

        string IStorageService.SecretAccessKey {
            set { _secretAccessKey = value; }
        }

        string[] IStorageService.EnumBuckets() 
        {
            DateTime time = GetCurrentTimeResolvedToMillis();
            string   sig  = MakeSignature("ListAllMyBuckets", time);

            ListAllMyBucketsResult res = _service.ListAllMyBuckets(_accessKeyId, time, true, sig);

            if(res.Buckets != null) {
                string[] ret = new string[res.Buckets.Length];
                for(int idx = 0; idx < res.Buckets.Length; idx++) {
                    ret[idx] = res.Buckets[idx].Name;
                }
                return ret;
            } else {
                return new string[0];
            }
        }

        void IStorageService.CreateBucket(string bucket, AWSGrant acl) 
        {
            Grant[]  acl2 = ConvertGrant(acl);
            DateTime time = GetCurrentTimeResolvedToMillis();
            string   sig  = MakeSignature("CreateBucket", time);

            _service.CreateBucket(bucket, acl2, _accessKeyId, time, true, sig);
        }

        void IStorageService.DeleteBucket(string bucket) 
        {
            DateTime time = GetCurrentTimeResolvedToMillis();
            string   sig  = MakeSignature("DeleteBucket", time);
            string   cred = null;

            _service.DeleteBucket(bucket, _accessKeyId, time, true, sig, cred);
        }

        AWSBucket IStorageService.ListBucket(string bucket, string prefix, string marker, int? maxkeys, string delimiter) 
        {
            DateTime time = GetCurrentTimeResolvedToMillis();
            string   sig  = MakeSignature("ListBucket", time);
            string   cred = null;

            ListBucketResult res = _service.ListBucket(bucket, prefix, marker, maxkeys.GetValueOrDefault(), maxkeys != null, delimiter, _accessKeyId, time, true, sig, cred);
            AWSBucket        ret = new AWSBucket();

            ret.IsTruncated = res.IsTruncated;

            if(res.Contents != null) {
                ret.Objects = new AWSObject[res.Contents.Length];
                for(int idx = 0; idx < res.Contents.Length; idx++) {
                    ListEntry res1 = res.Contents[idx];
                    ret.Objects[idx] = new AWSObject();
                    ret.Objects[idx].Key          = res1.Key;
                    ret.Objects[idx].Size         = res1.Size;
                    ret.Objects[idx].LastModified = res1.LastModified;
                }
            } else {
                ret.Objects = new AWSObject[0];
            }

            if(res.CommonPrefixes != null) {
                ret.ObjectPrefixes = new string[res.CommonPrefixes.Length];
                for(int idx = 0; idx < res.CommonPrefixes.Length; idx++) {
                    ret.ObjectPrefixes[idx] = res.CommonPrefixes[idx].Prefix;
                }
            } else {
                ret.ObjectPrefixes = new string[0];
            }

            return ret;
        }

        private AWSObject DoGetObject(string bucket, string key, bool getmeta, bool getdata) 
        {
            DateTime time = GetCurrentTimeResolvedToMillis();
            string   sig  = MakeSignature("GetObject", time);
            string   cred = null;

            GetObjectResult res = _service.GetObject(bucket, key, getmeta, getdata, true, _accessKeyId, time, true, sig, cred);
            AWSObject       ret = new AWSObject();

            ret.Key          = key;
            ret.Size         = (res.Data != null) ? res.Data.Length : 0;
            ret.LastModified = res.LastModified;
            ret.Data         = res.Data;

            if(res.Metadata != null) {
                ret.Metadata = new AWSMetadataEntry[res.Metadata.Length];
                for(int idx = 0; idx < res.Metadata.Length; idx++) {
                    MetadataEntry res1 = res.Metadata[idx];
                    ret.Metadata[idx] = new AWSMetadataEntry();
                    ret.Metadata[idx].MetaKey   = res1.Name;
                    ret.Metadata[idx].MetaValue = res1.Value;
                }
            } else {
                ret.Metadata = new AWSMetadataEntry[0];
            }

            return ret;
        }

        AWSObject IStorageService.GetObject(string bucket, string key, bool getmeta, bool getdata) 
        {
            return DoGetObject(bucket, key, getmeta, getdata);
        }

        Stream IStorageService.GetObjectStream(string bucket, string key)
        {
            AWSObject obj = DoGetObject(bucket, key, false, true);
            return new MemoryStream(obj.Data);
        }

        void IStorageService.CreateObject(string bucket, string key, AWSMetadataEntry[] meta, byte[] data, AWSGrant acl) 
        {
            Grant[]         acl2  = ConvertGrant(acl);
            MetadataEntry[] meta2 = ConvertMetadata(meta);
            DateTime        time  = GetCurrentTimeResolvedToMillis();
            string          sig   = MakeSignature("PutObjectInline", time);
            string          cred  = null;

            _service.PutObjectInline(bucket, key, meta2, data, data.Length, acl2, StorageClass.STANDARD, false, _accessKeyId, time, true, sig, cred);
        }

        Stream IStorageService.CreateObjectStream(string bucket, string key, long? size, AWSMetadataEntry[] meta, AWSGrant acl)
        {
            Grant[]         acl2  = ConvertGrant(acl);
            MetadataEntry[] meta2 = ConvertMetadata(meta);
            DateTime        time  = GetCurrentTimeResolvedToMillis();
            string          sig   = MakeSignature("PutObjectInline", time);
            string          cred  = null;

            return new SoapRequestStream(_service, bucket, key, meta2, acl2, _accessKeyId, time, sig, cred);
        }

        void IStorageService.DeleteObject(string bucket, string key) 
        {
            DateTime time = GetCurrentTimeResolvedToMillis();
            string   sig  = MakeSignature("DeleteObject", time);
            string   cred = null;

            _service.DeleteObject(bucket, key, _accessKeyId, time, true, sig, cred);
        }

        // *** 

        private Grant[] ConvertGrant(AWSGrant acl) 
        {
            Grant[] ret;
            switch(acl) {
                case AWSGrant.PrivateOnly:    
                    ret = null;
                    break;
                case AWSGrant.PublicRead: {
                    Group gra = new Group();
                    gra.URI = "http://acs.amazonaws.com/groups/global/AllUsers";
                    ret = new Grant[1];
                    ret[0] = new Grant();
                    ret[0].Grantee    = gra;
                    ret[0].Permission = Permission.READ;
                }   break;
                case AWSGrant.PublicWrite: {
                    Group gra = new Group();
                    gra.URI = "http://acs.amazonaws.com/groups/global/AllUsers";
                    ret = new Grant[1];
                    ret[0] = new Grant();
                    ret[0].Grantee    = gra;
                    ret[0].Permission = Permission.WRITE;
                }   break;
                default:
                    throw new ArgumentException();
            }
            return ret;
        }

        private MetadataEntry[] ConvertMetadata(AWSMetadataEntry[] meta) 
        {
            if(meta != null) {
                MetadataEntry[] ret = new MetadataEntry[meta.Length];
                for(int idx = 0; idx < meta.Length; idx++) {
                    string mkey = meta[idx].MetaKey;
                    string mval = meta[idx].MetaValue;
                    if(mkey.ToLower() != "content-type") {
                        mkey = mkey.ToLower();
                    } else {
                        mkey = "Content-Type";
                    }
                    ret[idx] = new MetadataEntry();
                    ret[idx].Name  = mkey;
                    ret[idx].Value = mval;
                }
                return ret;
            } else {
                return null;
            }
        }

        private static DateTime GetCurrentTimeResolvedToMillis() {
            DateTime time = DateTime.Now;
            return new DateTime(
                time.Year, time.Month,  time.Day, 
                time.Hour, time.Minute, time.Second, time.Millisecond, DateTimeKind.Local);
        }

        private static string FormatAsISO8601(DateTime time) {
            return time.ToUniversalTime().ToString(
                TIMESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
        }

        private string MakeSignature(string method, DateTime time) {
            string   s2s = SERVICE_NAME + method + FormatAsISO8601(time);
            Encoding ae  = new UTF8Encoding();
            HMACSHA1 sig = new HMACSHA1(ae.GetBytes(_secretAccessKey));
            return Convert.ToBase64String(sig.ComputeHash(ae.GetBytes(s2s.ToCharArray())));
        }

        private sealed class SoapRequestStream : Stream {

            private readonly AmazonS3        _service;
            private readonly string          _bucket;
            private readonly string          _key;
            private readonly MetadataEntry[] _meta2;
            private readonly Grant[]         _acl2;
            private readonly string          _accessKeyId;
            private readonly DateTime        _time;
            private readonly string          _sig;
            private readonly string          _cred;

            private readonly MemoryStream    _stm;

            public SoapRequestStream(AmazonS3 service, string bucket, string key, MetadataEntry[] meta2, Grant[] acl2, string accessKeyId, DateTime time, string sig, string cred) 
            {
                _service     = service;
                _bucket      = bucket;
                _key         = key;
                _meta2       = meta2;
                _acl2        = acl2;
                _accessKeyId = accessKeyId;
                _time        = time;
                _sig         = sig;
                _cred        = cred;

                _stm = new MemoryStream();
           }

            public override bool CanRead {
                get { return false; }
            }

            public override bool CanSeek {
                get { return false; }
            }

            public override bool CanWrite {
                get { return true; }
            }

            public override void Flush() { }

            public override long Length {
                get { throw new NotSupportedException(); }
            }

            public override long Position {
                get { throw new NotSupportedException(); }
                set { throw new NotSupportedException(); }
            }

            public override int Read(byte[] buffer, int offset, int count) {
                throw new NotSupportedException();
            }

            public override long Seek(long offset, SeekOrigin origin) {
                throw new NotSupportedException();
            }

            public override void SetLength(long value) {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count) {
                _stm.Write(buffer, offset, count);
            }

            public override void Close() {
                try {
                    byte[] data = _stm.ToArray();
                    _service.PutObjectInline(_bucket, _key, _meta2, data, data.Length, _acl2, StorageClass.STANDARD, false, _accessKeyId, _time, true, _sig, _cred);
                } finally {
                    base.Close();
                }
            }
        }
    }
}
