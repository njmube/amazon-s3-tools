using System;

namespace ch.ph.FileSystem {

    public interface IElement {

        String Name { get; }

        String FullName { get; }

        IDirectory Parent { get; }

        bool IsDirectory { get; }

        bool IsFile { get; }

        void Delete();
    }

}