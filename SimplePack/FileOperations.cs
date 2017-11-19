using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archive;
using SharpCompress.Archive.Rar;
using SharpCompress.Common;


/*
    AUTHOR: LuRRE


    DESCRIPTION:
        Drag and drop items onto .exe to print parent folder name or await the drag and drop onto the console window ASYNC.


    PARAMETERS:
        ARGS[] = The string array of files started with APP.
        ParentFolder = to be determined
        FileName = tbd
        FullPath = tbd

    RETURN:

*/
namespace SimplePack
{

    class FileOperations
    {
        public string ParentFolder { get; set; }
        public string FileName { get; set; }

        public string FullPath { get; set; }

        public string InitialInput(string[] args)
        {
            if (args.Length > 0 && File.Exists(args[0]))
            {
                foreach (var item in args)
                {
                    string path = args[0];
                    using (StreamReader sr = new StreamReader(path))
                    {
                        //Prints parent folder of file, later used for renaming
                        string parentfolder = Directory.GetParent(item).Name;
                        string filename = Path.GetFileName(item);
                        string fullpath = Path.GetFullPath(item);
                        Console.WriteLine("Extracting {1} to {0}", parentfolder, filename);
                        Console.WriteLine("Extension: {0}\n", Path.GetExtension(item));
                        //Saving strings to respective properties
                        FullPath = fullpath;
                        FileName = filename;
                        ParentFolder = parentfolder;
                    }
                }
                return ParentFolder;
            }
            Console.WriteLine("No input found.\nDrag and drop files onto ezpack.exe");
            Console.ReadKey();
            return null;
        }

        public void Unrar(string[] args)
        {
            int extracted = 0;
            try
            {
                if (!isArgs(args)) return;
                foreach (var item in args)
                {
                    string path = args[0];
                    using (StreamReader sr = new StreamReader(path))
                    {
                        if (Path.GetExtension(item) == ".zip") ZipFile.ExtractToDirectory(item, ParentFolder);
                        Console.WriteLine("Unpacking: {0}\n", FullPath);
                        extracted++;

                        //if (Path.GetExtension(item) == ".rar") RarArchive.Open(sr)
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: File already exists\n + {0}", e);

            }

            finally
            {
                Console.WriteLine("{0} archive(s) extracted.", extracted);
                Console.ReadLine();
            }
        }

        public bool isArgs(string[] args)
        {
            foreach (var item in args)
            {
                if (Path.GetExtension(item) != ".zip" && Path.GetExtension(item)  != ".rar")
                {
                    Console.WriteLine("Error: File extension must be .zip or .rar");
                    return false;
                }
            }
            return true;
        }
    }
}