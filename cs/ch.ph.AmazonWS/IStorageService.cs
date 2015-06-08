using System;
using System.IO;

namespace ch.ph.AmazonWS
{
    public interface IStorageService
    {
        string AccessKeyId     { set; }
        string SecretAccessKey { set; }

        string[] EnumBuckets();

        void CreateBucket(string bucket, AWSGrant acl);

        void DeleteBucket(string bucket);

        AWSBucket ListBucket(string bucket, string prefix, string marker, int? maxkeys, string delimiter);

        AWSObject GetObject(string bucket, string key, bool getmeta, bool getdata);

        Stream GetObjectStream(string bucket, string key);

        void CreateObject(string bucket, string key, AWSMetadataEntry[] meta, byte[] data, AWSGrant acl);

        Stream CreateObjectStream(string bucket, string key, long? size, AWSMetadataEntry[] meta, AWSGrant acl);

        void DeleteObject(string bucket, string key);
    }
}
