using System;
using System.IO;

namespace ch.ph.FileSystem {

    public interface IFile : IElement {

        long Size { get; }

        string ContentType { get; }

        Stream Open(bool read, bool write);
    }
}