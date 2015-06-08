using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Security.Cryptography;

namespace ch.ph.AmazonWS
{
    public sealed class RestStorageService : IStorageService
    {
        private static string SERVICE_URL      = "http://s3.amazonaws.com/";
        private static string SERVICE_NS1      = "http://s3.amazonaws.com/doc/2006-03-01/";
        private static string TIMESTAMP_FORMAT = "r";

        private string _accessKeyId;
        private string _secretAccessKey;

        // *** 

        public RestStorageService() { }

        // *** IStorageService

        string IStorageService.AccessKeyId {
            set { _accessKeyId = value; }
        }

        string IStorageService.SecretAccessKey {
            set { _secretAccessKey = value; }
        }

        string[] IStorageService.EnumBuckets() 
        {
            string time = GetCurrentTime();
            string s2s  = "GET\n" +
                          "\n" +
                          "\n" + 
                          "\n" +
                          "x-amz-date:" + time + "\n" + 
                          "/";

            string sig = MakeSignature(s2s);

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL);
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            HttpWebResponse res = (HttpWebResponse) req.GetResponse();
            List<string>    ret = new List<string>();

            try {
                if(res.ContentType != "application/xml" && res.ContentType != "text/xml") {
                    throw new Exception("Response does not contain XML data.");
                }
                
                XmlDocument xml = new XmlDocument();
                xml.Load(res.GetResponseStream());

                XPathNavigator nav = xml.CreateNavigator();
                nav.MoveToChild("ListAllMyBucketsResult", SERVICE_NS1);
                nav.MoveToChild("Buckets", SERVICE_NS1);

                if(nav.MoveToChild("Bucket", SERVICE_NS1)) {
                    do {
                        if(nav.MoveToChild("Name", SERVICE_NS1)) {
                            ret.Add(nav.Value);
                            nav.MoveToParent();
                        }
                    } while(nav.MoveToNext("Bucket", SERVICE_NS1));
                    nav.MoveToParent();
                }
                
                return ret.ToArray();

            } finally {
                res.Close();
            }
        }

        void IStorageService.CreateBucket(string bucket, AWSGrant acl) 
        {
            string acl2 = ConvertGrant(acl);
            string time = GetCurrentTime();
            string s2s  = "PUT\n" +
                          "\n" +
                          "\n" + 
                          "\n" +
                          "x-amz-acl:" + acl2 + "\n" +
                          "x-amz-date:" + time + "\n" + 
                          "/" + bucket + "/";

            string sig = MakeSignature(s2s);

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL + bucket + "/");
            req.Method = "PUT";
            req.Headers["x-amz-acl"]     = acl2;
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            req.GetResponse().Close();
        }

        void IStorageService.DeleteBucket(string bucket) 
        {
            string time = GetCurrentTime();
            string s2s  = "DELETE\n" +
                          "\n" +
                          "\n" + 
                          "\n" +
                          "x-amz-date:" + time + "\n" + 
                          "/" + bucket + "/";

            string sig = MakeSignature(s2s);

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL + bucket + "/");
            req.Method = "DELETE";
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            req.GetResponse().Close();
        }

        AWSBucket IStorageService.ListBucket(string bucket, string prefix, string marker, int? maxkeys, string delimiter) 
        {
            string time = GetCurrentTime();
            string s2s  = "GET\n" +
                          "\n" +
                          "\n" + 
                          "\n" +
                          "x-amz-date:" + time + "\n" + 
                          HttpUtility.UrlPathEncode("/" + bucket + "/");

            string sig = MakeSignature(s2s);
            string qry = "";

            if(prefix != null)    qry = qry + (qry.Length==0 ? "?" : "&") + "prefix="    + prefix;
            if(marker != null)    qry = qry + (qry.Length==0 ? "?" : "&") + "marker="    + marker;
            if(maxkeys.HasValue)  qry = qry + (qry.Length==0 ? "?" : "&") + "max-keys="  + maxkeys.Value;
            if(delimiter != null) qry = qry + (qry.Length==0 ? "?" : "&") + "delimiter=" + delimiter;

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL + HttpUtility.UrlPathEncode(bucket + "/") + qry);
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            HttpWebResponse res = (HttpWebResponse) req.GetResponse();
            AWSBucket       ret = new AWSBucket();

            try {
                if(res.ContentType != "application/xml" && res.ContentType != "text/xml") {
                    throw new Exception("Response does not contain XML data.");
                }
                
                XmlDocument xml = new XmlDocument();
                xml.Load(res.GetResponseStream());

                XPathNavigator nav = xml.CreateNavigator();
                nav.MoveToChild("ListBucketResult", SERVICE_NS1);

                if(nav.MoveToChild("IsTruncated", SERVICE_NS1)) {
                    ret.IsTruncated = nav.ValueAsBoolean;
                    nav.MoveToParent();
                }
                
                if(nav.MoveToChild("Contents", SERVICE_NS1)) {
                    List<AWSObject> retobj = new List<AWSObject>();
                    do {
                        AWSObject obj = new AWSObject();
                        if(nav.MoveToChild("Key", SERVICE_NS1)) {
                            obj.Key = nav.Value;
                            nav.MoveToParent();
                        }
                        if(nav.MoveToChild("Size", SERVICE_NS1)) {
                            obj.Size = nav.ValueAsLong;
                            nav.MoveToParent();
                        }
                        if(nav.MoveToChild("LastModified", SERVICE_NS1)) {
                            obj.LastModified = nav.ValueAsDateTime;
                            nav.MoveToParent();
                        }
                        retobj.Add(obj);
                    } while(nav.MoveToNext("Contents", SERVICE_NS1));
                    nav.MoveToParent();
                    ret.Objects = retobj.ToArray();
                } else {
                    ret.Objects = new AWSObject[0];
                }
      
                if(nav.MoveToChild("CommonPrefixes", SERVICE_NS1)) {
                    List<string> retpref = new List<string>();
                    do {
                        if(nav.MoveToChild("Prefix", SERVICE_NS1)) {
                            retpref.Add(nav.Value);
                            nav.MoveToParent();
                        }
                    } while(nav.MoveToNext("CommonPrefixes", SERVICE_NS1));
                    nav.MoveToParent();
                    ret.ObjectPrefixes = retpref.ToArray();
                } else {
                    ret.ObjectPrefixes = new string[0];
                }
            
                return ret;

            } finally {
                res.Close();
            }
        }

        private void DoGetObject(string bucket, string key, bool getmeta, bool getdata, out AWSObject obj, out HttpWebResponse res) 
        {
            string time = GetCurrentTime();
            string s2s  = (getdata ? "GET\n" : "HEAD\n") +
                          "\n" +
                          "\n" + 
                          "\n" +
                          "x-amz-date:" + time + "\n" + 
                          HttpUtility.UrlPathEncode("/" + bucket + "/" + key);

            string sig = MakeSignature(s2s);

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL + HttpUtility.UrlPathEncode(bucket + "/" + key));
            req.Method                   = (getdata ? "GET" : "HEAD");
            req.ReadWriteTimeout         = -1;
            req.Timeout                  = -1;
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            res = (HttpWebResponse) req.GetResponse();
            
            obj = new AWSObject();
            obj.Key          = key;
            obj.LastModified = res.LastModified;

            if(getmeta) {
                List<AWSMetadataEntry> retmeta = new List<AWSMetadataEntry>();
                foreach(string hdr in res.Headers.AllKeys) {
                    if(hdr.StartsWith("x-amz-meta-")) {
                        string mkey = hdr.Substring(11);
                        string mval = res.Headers[hdr];
                        if(mkey != "content-type") {
                            AWSMetadataEntry meta = new AWSMetadataEntry(); 
                            meta.MetaKey   = mkey; 
                            meta.MetaValue = mval;
                            retmeta.Add(meta);
                        }
                    }
                }
                if(res.Headers["Content-Type"] != null) {
                    AWSMetadataEntry meta = new AWSMetadataEntry(); 
                    meta.MetaKey   = "Content-Type";
                    meta.MetaValue = res.Headers["Content-Type"];
                    retmeta.Add(meta);
                }
                obj.Metadata = retmeta.ToArray();
            } else {
                obj.Metadata = new AWSMetadataEntry[0];
            }

            if(getdata) {
                obj.Size = res.ContentLength;
            }
        }

        AWSObject IStorageService.GetObject(string bucket, string key, bool getmeta, bool getdata)
        {
            AWSObject       obj;
            HttpWebResponse res = null;

            try {
                DoGetObject(bucket, key, getmeta, getdata, out obj, out res);

                if(getdata) {
                    obj.Data = new byte[obj.Size];
                    int num = (int) obj.Size;
                    int pos = 0;
                    while(num > 0) {
                        int now = res.GetResponseStream().Read(obj.Data, pos, num);
                        num -= now;
                        pos += now;
                    }
                }

                return obj;

            } finally {
                if(res != null) res.Close();
            }
        }

        Stream IStorageService.GetObjectStream(string bucket, string key)
        {
            AWSObject       obj;
            HttpWebResponse res = null;

            try { 
                DoGetObject(bucket, key, false, true, out obj, out res);

                return new HttpResponseStream(res);

            } catch(Exception ex) {
                res.Close();
                throw ex;
            }
        }

        private HttpWebRequest DoCreateObject(string bucket, string key, AWSMetadataEntry[] meta, AWSGrant acl)
        {
            string acl2  = ConvertGrant(acl);
            string meta2 = ConvertMetadata4Sig(meta);
//          string cmd5  = MakeHash(data);
            string ctyp  = GetContentType(meta);
            string time  = GetCurrentTime();
            string s2s   = "PUT\n" +
                          /* cmd5 + */ "\n" +
                          ctyp + "\n" + 
                          "\n" +
                          "x-amz-acl:" + acl2 + "\n" +
                          "x-amz-date:" + time + "\n" + 
                          meta2 +
                          HttpUtility.UrlPathEncode("/" + bucket + "/" + key);

            string sig = MakeSignature(s2s);

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL + HttpUtility.UrlPathEncode(bucket + "/" + key));
            req.Method                   = "PUT";
            req.ContentType              = ctyp; 
            req.ReadWriteTimeout         = -1; // Not setting to -1 doesnt help a shit
            req.Timeout                  = -1;
//          req.ContentLength            = data.Length;
//          req.AllowWriteStreamBuffering = false; // Setting to false doesnt help a shit
            req.Headers["x-amz-acl"]     = acl2;
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            if(meta != null) {
                foreach(AWSMetadataEntry metaety in meta) {
                    if(metaety.MetaKey.ToLower() != "content-type")
                        req.Headers["x-amz-meta-" + metaety.MetaKey.ToLower()] = metaety.MetaValue;
                }
            }

            return req;
        }

        void IStorageService.CreateObject(string bucket, string key, AWSMetadataEntry[] meta, byte[] data, AWSGrant acl) 
        {
            HttpWebRequest req = DoCreateObject(bucket, key, meta, acl);
            Stream         stm = req.GetRequestStream();

            try {
                stm.Write(data, 0, data.Length);
            } finally {
                stm.Close();
                req.GetResponse().Close();
            }
        }

        Stream IStorageService.CreateObjectStream(string bucket, string key, long? size, AWSMetadataEntry[] meta, AWSGrant acl)
        {
            HttpWebRequest req = DoCreateObject(bucket, key, meta, acl);

            if(size.HasValue) {
                req.ContentLength = size.Value;
            }

            return new HttpRequestStream(req);
        }

        void IStorageService.DeleteObject(string bucket, string key) 
        {
            string time = GetCurrentTime();
            string s2s  = "DELETE\n" +
                          "\n" +
                          "\n" + 
                          "\n" +
                          "x-amz-date:" + time + "\n" + 
                          HttpUtility.UrlPathEncode("/" + bucket + "/" + key);
            
            string sig = MakeSignature(s2s);

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(SERVICE_URL + HttpUtility.UrlPathEncode(bucket + "/" + key));
            req.Method = "DELETE";
            req.Headers["x-amz-date"]    = time;
            req.Headers["Authorization"] = "AWS " + _accessKeyId + ":" + sig;

            req.GetResponse().Close();
        }

        // *** 

        private string ConvertGrant(AWSGrant acl) 
        {
            switch(acl) {
                case AWSGrant.PrivateOnly:    
                    return "private";
                case AWSGrant.PublicRead:
                    return "public-read";
                case AWSGrant.PublicWrite:
                    return "public-read-write";
                default:
                    throw new ArgumentException();
            }
        }

        private string ConvertMetadata4Sig(AWSMetadataEntry[] meta) {
            if(meta == null) {
                return "";
            } else {
                string ret = "";
                foreach(AWSMetadataEntry metaety in meta) {
                    if(metaety.MetaKey.ToLower() != "content-type")
                        ret = ret + "x-amz-meta-" + metaety.MetaKey.ToLower() + ":" + metaety.MetaValue + "\n";
                }
                return ret;
            }
        }

        private string GetContentType(AWSMetadataEntry[] meta) {
            if(meta != null) {
                foreach(AWSMetadataEntry metaety in meta) {
                    if(metaety.MetaKey.ToLower() == "content-type") 
                        return metaety.MetaValue;
                }
            }
            return "";
        }

        private static string GetCurrentTime() {
            return DateTime.UtcNow.ToString(
                TIMESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);        
        }

        private string MakeSignature(string s2s) {
            Encoding ae  = new UTF8Encoding();
            HMACSHA1 sig = new HMACSHA1(ae.GetBytes(_secretAccessKey));
            return Convert.ToBase64String(sig.ComputeHash(ae.GetBytes(s2s.ToCharArray())));
        }

        /* private string MakeHash(byte[] data) {
            MD5 sig = new MD5CryptoServiceProvider();
            return Convert.ToBase64String(sig.ComputeHash(data));
        } */

        private sealed class HttpRequestStream : Stream {

            private readonly HttpWebRequest _req;
            private readonly Stream         _stm;

            public HttpRequestStream(HttpWebRequest req) {
                _req = req;
                _stm = _req.GetRequestStream();
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

            public override void Flush() { 
                _stm.Flush();
            }

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
                    _stm.Close();
                    try { 
                        _req.GetResponse().Close();
                    } catch(WebException ex) {
                        HttpWebResponse res = (HttpWebResponse) ex.Response;
                        StreamReader rdr = new StreamReader(res.GetResponseStream());
                        string msg = rdr.ReadToEnd();                 
                        res.Close();
                        rdr.Close();
                        throw new Exception(ex.Message + "\n" + msg, ex);
                    }
                } finally {
                    base.Close();
                }
            }

            //protected override void Dispose(bool disposing) {
            //    try {
            //        if(disposing) {
            //            _stm.Close();
            //            _req.GetResponse().Close();
            //        }
            //    } finally {
            //        base.Dispose(disposing);
            //    }
            //}
        }

        private sealed class HttpResponseStream : Stream {

            private readonly HttpWebResponse _res;
            private readonly Stream          _stm;

            public HttpResponseStream(HttpWebResponse res) { 
                _res = res;
                _stm = _res.GetResponseStream();
            }

            public override bool CanRead {
                get { return true; }
            }

            public override bool CanSeek {
                get { return false; }
            }

            public override bool CanWrite {
                get { return false; }
            }

            public override void Flush() { }

            public override long Length {
                get { return _res.ContentLength; }
            }

            public override long Position {
                get { throw new NotSupportedException(); }
                set { throw new NotSupportedException(); }
            }

            public override int Read(byte[] buffer, int offset, int count) {
                return _stm.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin) {
                throw new NotSupportedException();
            }

            public override void SetLength(long value) {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count) {
                throw new NotSupportedException();
            }

            public override void Close() {
                try {
                    _res.Close(); // calling both _stm.Close() and _res.Close() is not necessary
                } finally {
                    base.Close();
                }
            }

            //protected override void Dispose(bool disposing) {
            //    try {
            //        if(disposing) {
            //            _res.Close(); // calling both _stm.Close() and _res.Close() is not necessary
            //        }
            //    } finally {
            //        base.Dispose(disposing);
            //    }
            //}
        }
    }
}
