using System;
using System.Collections.Generic;
using System.IO;

namespace ch.ph.FileSystem {

    public interface IDirectory : IElement {

        IList<IDirectory> GetDirectories();

        IList<IFile> GetFiles();

        IElement GetChild(string name);

        IDirectory CreateDirectory(string name);

        Stream CreateFileAndOpen(string name, long? size, string contentType, bool read, bool write);
    }
}