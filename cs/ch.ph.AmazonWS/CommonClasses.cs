using System;

namespace ch.ph.AmazonWS
{
    public enum AWSGrant {
        PrivateOnly = 1,
        PublicRead  = 2,
        PublicWrite = 3
    }

    public sealed class AWSMetadataEntry {
        public string MetaKey;
        public string MetaValue;
    }

    public sealed class AWSBucket {
        public bool        IsTruncated;
        public AWSObject[] Objects;
        public string[]    ObjectPrefixes;
    }

    public sealed class AWSObject {
        public string             Key;
        public long               Size;
        public DateTime           LastModified;
        public byte[]             Data;
        public AWSMetadataEntry[] Metadata;
    }
}