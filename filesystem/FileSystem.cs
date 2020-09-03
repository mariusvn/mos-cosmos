using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;

namespace MOS
{
    class FileSystem
    {
        CosmosVFS vfs;
        public FileSystem()
        {
            Console.WriteLine("Initializing file system ...");
            this.vfs = new CosmosVFS();
            VFSManager.RegisterVFS(this.vfs);
            Console.WriteLine("File system initialized !");
        }
    }
}
