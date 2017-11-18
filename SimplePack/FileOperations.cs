using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;


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
                CheckArgs(args);

                foreach (var item in args)
                {

                    string path = args[0];
                    using (StreamReader sr = new StreamReader(path))
                    {
                        //Extract zip from item location to parent folder.
                        ZipFile.ExtractToDirectory(item, ParentFolder);
                        Console.WriteLine("Unpacking: {0}", FullPath);
                        extracted++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File already exists\n");

            }

            finally
            {


                Console.WriteLine("{0} archive(s) extracted.", extracted);
                Console.ReadLine();
            }
        }

        public bool CheckArgs(string[] args)
        {
            foreach (var item in args)
            {
                if (Path.GetExtension(item) != ".zip")
                {
                    Console.WriteLine("File extension must be .zip");
                    return false;
                }
            }

            return true;
        }

    }
}