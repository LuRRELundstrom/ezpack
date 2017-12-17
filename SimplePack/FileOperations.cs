using System;
using System.Collections.Generic;
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


    FIELDS:
        ARGS[] = The string array of command line inputs.
        TempParent = Used for storing temporary extraction path.
        ArchivesExtracted = selfExplanatory
        FilesExtracted = selfExplanatory
        ValidRar = String-array of compatible .rar 
        ValidZip = String-array of compatible .zip

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
        private string TempParent { get; set; }
        private int FilesExtracted { get; set; }
        private int ArchivesExtracted { get; set; }
        private double TotalSize { get; set; }
        private string[] ValidRar { get; set; }
        private string[] ValidZip { get; set; }
        private ArchiveExtension ArchiveExtension { get; set; }
        private string[] ExcludedFiles { get; set; }
        private string[] Args { get; set; }


        public string CheckInput(string[] args)
        {
            //Nullcheck
            //Todo Fix commandline input, pass arguments to processing.
            if (args.Length > 0 && args.All(File.Exists))
            {
                TempParent = Directory.GetParent(args[0]).Name;
                return TempParent;
            }
            Console.WriteLine("No input found.\nDrag and drop files onto ezpack.exe");
            var list = new List<string>
            {
                Console.ReadLine()
            };
            Args = list.ToArray();
            TempParent = Directory.GetParent(args[0]).Name;
            return TempParent;
            
        }
        //Extracting .ZIP with overwrite permission
        public void Unzip(string[] zipFiles)
        {
            var helper = new PrintOperations(TotalSize);
            foreach (var archive in zipFiles)
            {
                helper.PrintExtracting(archive);
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
            helper.PrintExtracted(FilesExtracted, ArchivesExtracted);
        }

        //Extracting .RAR with overwrite permission
        public void Unrar(string[] rarFiles)
        {
            var helper = new PrintOperations();
            foreach (var archive in rarFiles)
            {
                helper.PrintExtracting(archive);
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
            helper.PrintExtracted(FilesExtracted, ArchivesExtracted);
        }

        //Sort files into string arrays of valid extensions with
        public void ValidateFiles(string[] args)
        {
            ValidRar = Array.FindAll(args, f => string.Equals(Path.GetExtension(f), ".rar", StringComparison.OrdinalIgnoreCase) ||
                                                string.Equals(Path.GetExtension(f), ".r00", StringComparison.OrdinalIgnoreCase));
            ValidZip = Array.FindAll(args, f => string.Equals(Path.GetExtension(f), ".zip", StringComparison.OrdinalIgnoreCase));

            ExcludedFiles = Array.FindAll(args, f => !string.Equals(Path.GetExtension(f), ".zip", StringComparison.OrdinalIgnoreCase) ||
                                                     !string.Equals(Path.GetExtension(f), ".r00", StringComparison.OrdinalIgnoreCase) ||
                                                     !string.Equals(Path.GetExtension(f), ".rar", StringComparison.OrdinalIgnoreCase));

            /*Alternative solutions
           
                1.   var ValidRar = (from item in args
                                where string.Equals(Path.GetExtension(f), ".zip", StringComparison.OrdinalIgnoreCase) || 
                                      Path.GetExtension(item) == ".r00"
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
            try
            {
                var helper = new PrintOperations(TotalSize);
                switch (ArchiveExtension)
                {
                    case ArchiveExtension.Rar:
                        helper.GetSize(ValidRar);
                        helper.PrintSize();
                        Unrar(ValidRar);
                        helper.PrintExcluded(ExcludedFiles);
                        break;
                    case ArchiveExtension.Zip:
                        helper.GetSize(ValidZip);
                        helper.PrintSize();
                        Unzip(ValidZip);
                        helper.PrintExcluded(ExcludedFiles);
                        break;
                    case ArchiveExtension.Invalid:
                        Console.WriteLine("Valid extensions: .zip, .rar");
                        helper.PrintExcluded(ExcludedFiles);
                        Console.ReadLine();
                        //throw new FileNotFoundException("Error: File extension must be .zip or .rar");
                        break;
                }
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine("\nFile is corrupt. Exiting..\n\n" + e);
                Console.ReadLine();
            }

            finally
            {
                System.Environment.Exit(0);
            }
        }
    }
}
