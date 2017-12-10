using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Readers;
using SharpCompress.Readers.Zip;
using SharpCompress.Writers.Zip;


/*
    AUTHOR: LuRRE

    DESCRIPTION:
        Class containing methods to Unrar, unzip, and various checks on input parameters ARGS[]. 


    PARAMETERS:
        ARGS[] = The string array of files started with APP.
        TempParent = parentfolder of archive, will change to list later
        ArchivesExtracted = selfExplanatory
        FilesExtracted = selfExplanatory
        ValidRar = Checked for .rar 
        ValidZip = Checked for .zip

    RETURN:

*/
namespace EZpack
{
    //Compatible extensions. Default value (invalid) is always first!
    internal enum ArchiveExtension
    {
        Invalid,
        Zip,
        Rar,
    }
    internal class FileOperations
    {

        //Auto-property fields
        public string TempParent { get; set; }
        public int FilesExtracted { get; set; }
        public int ArchivesExtracted { get; set; }
        public long TotalSize { get; set; }
        public long SizeRead { get; set; }
        public string[] ValidRar { get; set; }
        public string[] ValidZip { get; set; }
        public ArchiveExtension ArchiveExtension { get; set; }
        public string[] ExcludedFiles { get; set; }
        

        public string CheckInput(string[] args)
        {
            //Nullcheck

            if (args.Length > 0 && args.All(File.Exists))
            {
                TempParent = Directory.GetParent(args[0]).Name;
                return TempParent;
            }
            Console.WriteLine("No input found.\nDrag and drop files onto ezpack.exe");
            Console.ReadLine();
            return null;
        }
        //Extracting .ZIP with overwrite permission
        public void Unzip(string[] zipFiles)
        {
            foreach (var archive in zipFiles)
            {
                PrintExtracting(archive);
                TempParent = Directory.GetParent(archive).Name;
                using (var openZip = ZipFile.OpenRead(archive))
                {
                    foreach (var item in openZip.Entries)
                    {
                        if (!File.Exists(TempParent))
                        {
                            Directory.CreateDirectory(TempParent);
                        }
                        var newPath = Path.Combine(TempParent, item.ToString());
                        item.ExtractToFile(newPath, overwrite: true);
                        FilesExtracted++;
                    }
                }
                ArchivesExtracted++;
            }
            PrintExtracted();
        }

        //Extracting .RAR with overwrite permission
        public void Unrar(string[] rarFiles)
        {
            foreach (var archive in rarFiles)
            {
                PrintExtracting(archive);
                //Set extraction path for each archive.
                TempParent = Directory.GetParent(archive).Name;
                using (var openrar = RarArchive.Open(archive))
                {
                    foreach (var item in openrar.Entries)
                    {
                        item.WriteToDirectory(TempParent, new ExtractionOptions() { Overwrite = true, ExtractFullPath = true });
                        FilesExtracted++;
                    }
                }
                ArchivesExtracted++;
            }
            PrintExtracted();
        }

        //Iterates all archives to verify that they are compatible
        public void ValidateFiles(string[] args)
        {
            //Sort files into arrays of  valid RAR and ZIP to save for extraction
            ValidRar = Array.FindAll(args, f => Path.GetExtension(f) == ".rar" || Path.GetExtension(f) == ".r00");
            ValidZip = Array.FindAll(args, f => Path.GetExtension(f) == ".zip");
            ExcludedFiles = Array.FindAll(args, f => Path.GetExtension(f) != ".zip" ||
                                                     Path.GetExtension(f) != ".r00" ||
                                                     Path.GetExtension(f) != ".rar");
           

                    /*Alternative solutions
           
                     1.   var ValidRar = (from item in args
                                        where Path.GetExtension(item) == ".rar" || Path.GetExtension(item) == ".r00"
                                        select item).ToArray();

                     2.   var ValidZip = args
                            .Where(item => Path.GetExtension(item) == ".rar" || Path.GetExtension(item) == ".r00")
                            .ToArray();
                    */

            //Setting extraction state
            if (ValidZip.Length > 0)
            {
                ArchiveExtension = ArchiveExtension.Zip;
            }
            else if (ValidRar.Length > 0)
            {
                ArchiveExtension = ArchiveExtension.Rar;
            }
            else
            {
                ArchiveExtension = ArchiveExtension.Invalid;
            }
        }

        //Determines which method to use for extraction
        public void DetermineExtract(string[] args)
        {
            switch (ArchiveExtension)
            {
                case ArchiveExtension.Rar:
                    CalculateSize(ValidRar);
                    PrintSize();
                    Unrar(ValidRar);
                    PrintExcluded();
                    break;
                case ArchiveExtension.Zip:
                    CalculateSize(ValidZip);
                    PrintSize();
                    Unzip(ValidZip);
                    PrintExcluded();
                    break;
                case ArchiveExtension.Invalid:
                    Console.WriteLine("File Extension must be .zip or .rar");
                    PrintExcluded();
                    Console.ReadLine();
                    //throw new FileNotFoundException("Error: File extension must be .zip or .rar");
                    break;
            }
        }

        public long CalculateSize(string[] files)
        {
            foreach (var file in files)
            {
                var f = new FileInfo(file);
                TotalSize += f.Length;
            }
            return TotalSize;
        }

        public void PrintSize()
        {
            //bytes to kilobytes
            TotalSize = TotalSize / 1000;
            Console.WriteLine("Total size: {0} kb", TotalSize);
        }

        public void PrintExtracted()
        {
            Console.WriteLine("{0} file(s) in {1} archive(s) extracted.", FilesExtracted, ArchivesExtracted);
            Console.ReadLine();

        }

        public void PrintExcluded()
        {
            if (ExcludedFiles.Length == 0) return;
            Console.WriteLine("Following file(s) are not eligible for extraction:\n");
            foreach (var item in ExcludedFiles)
            {
                Console.WriteLine(item);
            }
        }

        public void PrintExtracting(string file)
        {
            Console.WriteLine($"Extracting: {file}");
        }
    }
}
