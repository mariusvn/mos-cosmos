using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;
using System.Text;

namespace MOS
{
    public class FileSystem
    {
        CosmosVFS vfs;

        public FileSystem()
        {
            Console.WriteLine("Initializing file system ...");
            this.vfs = new CosmosVFS();
            VFSManager.RegisterVFS(this.vfs);
            Console.WriteLine("File system initialized !");
        }

        public byte[] readFile(string path)
        {
            try
            {
                DirectoryEntry entry = this.vfs.GetFile(@"0:\Kudzu.txt");
                Stream stream = entry.GetFileStream();
                if (stream.CanRead)
                {
                    byte[] content = new byte[stream.Length];
                    stream.Read(content, 0, (int)stream.Length);
                    return content;
                } 
                else
                {
                    throw new IOException("Bad ACL");
                }
            } 
            catch (Exception e)
            {
                throw new IOException("Cannot read \"" + path + "\": " + e.Message);
            }

        }

        public string readFileAsString(string path)
        {
            return Encoding.Default.GetString(this.readFile(path));
        }
    }
}
