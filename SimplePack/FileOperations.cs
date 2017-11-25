using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Readers;


/*
    AUTHOR: LuRRE


    DESCRIPTION:
        Class containing methods to Unrar, unzip, and various checks on input parameters ARGS[]. 


    PARAMETERS:
        ARGS[] = The string array of files started with APP.
        ParentFolder = parentfolder of archive, will change to list later
        ArchivesExtracted = selfExplanatory
        FilesExtracted = selfExplanatory
        ListOfArchives = A list of all the archives of .rar or .zip to extract.

    RETURN:

*/
namespace SimplePack
{

    internal class FileOperations
    {
        public string ParentFolder { get; set; }
        public int FilesExtracted { get; set; }
        public int ArchivesExtracted { get; set; }

        public string InitialInput(string[] args)
        {
            //Nullcheck
            if (args.Length > 0 && File.Exists(args[0]))
            {
                ParentFolder = Directory.GetParent(args[0]).Name;
                return ParentFolder;
            }
            Console.WriteLine("No input found.\nDrag and drop files onto ezpack.exe");
            Console.ReadKey();
            return null;
        }
        //Extracting .ZIP without overwrite permission
        public void Unzip(string[] zipFiles)
        {
            try
            {
                foreach (var file in zipFiles)
                {
                    ParentFolder = Directory.GetParent(file).Name;
                    using (var sr = new StreamReader(file))
                    {
                        {
                            Console.WriteLine("Extracting: {0}", file);
                            ZipFile.ExtractToDirectory(file, ParentFolder);
                            FilesExtracted++;
                        }
                    }
                    ArchivesExtracted++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: File already exists\n + {0}", e);
            }

            finally
            {
                Console.WriteLine("{0} file(s) in {1} archive(s) extracted.", FilesExtracted, ArchivesExtracted);
                Console.ReadLine();
            }
        }

        //Extracting .RAR with overwrite permission
        public void Unrar(string[] rarFiles)
        {
            foreach (var archive in rarFiles)
            {
                //Set extraction path for each archive.
                ParentFolder = Directory.GetParent(archive).Name;
                using (var openrar = RarArchive.Open(archive))
                {
                    foreach (var item in openrar.Entries)
                    {
                        item.WriteToDirectory(ParentFolder, new ExtractionOptions() { Overwrite = true, ExtractFullPath = true });
                        FilesExtracted++;
                    }
                }
                Console.WriteLine("Extracting: {0}", archive);
                ArchivesExtracted++;
            }
            Console.WriteLine("{0} file(s) in {1} archive(s) extracted.", FilesExtracted, ArchivesExtracted);
            Console.ReadLine();
        }

        //Iterates archives to verify the correct extraction method
        public void DetermineExtract(string[] args)
        {
            //Zip iteration LINQ
            if (args.Any(item => Path.GetExtension(item) == ".zip")) Unzip(args);
            //Rar iteration LINQ 
            else if (args.Any(item => Path.GetExtension(item) == ".rar" || Path.GetExtension(item) == ".r00")) Unrar(args);
            else
            {
                Console.WriteLine("Error: File extension must be .zip or .rar");
                Console.ReadLine();
            }
        } 
    }
}
