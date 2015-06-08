using System;
using ch.ph.FileSystem;

namespace ch.ph.FileSystem.LocalFileSystem {

    public class FileSystem : IFileSystem {

        // *** Private Attributes *******************************************

        private readonly DirectoryImpl _root;

        // *** Public Methods ***********************************************

        public FileSystem(string rootDir) {
            _root = new DirectoryImpl(null, rootDir, rootDir);
        }

        // *** IFileSystem **************************************************

        string IFileSystem.TypeName { 
            get { return "Local File System"; }
        }

        IDirectory IFileSystem.RootDirectory {
            get { return _root; }
        }
    }

}