using System;

namespace ch.ph.FileSystem {

    public interface IFileSystem {

        string TypeName { get; }

        IDirectory RootDirectory { get ; }

    }

}