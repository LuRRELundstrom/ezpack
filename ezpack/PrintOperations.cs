using System;
using System.IO;
/*
    AUTHOR: LuRRE

    DESCRIPTION:
        Class containing Print methods. 


    FIELDS:
        _totalSize = Constructs with a totalsize parameter. Used for calculating, printing and formatting bytesize.

    
    RETURN:

*/
namespace EZpack
{
    public class PrintOperations
    {

        //private fields
        private double _totalSize;

        public PrintOperations()
        {

        }
        public PrintOperations(double size)
        {
            this._totalSize = size;
        }
        public void PrintExtracted(int filesExtracted, int archivesExtracted)
        {
            Console.WriteLine("\n{0} file(s) in {1} archive(s) extracted.", filesExtracted, archivesExtracted);
        }
        public double GetSize(string[] files)
        {
            foreach (var file in files)
            {
                var f = new FileInfo(file);
                _totalSize += f.Length;
            }
            return _totalSize;
        }

        public void PrintSize()
        {
            //Formatting bytesize to correct suffix.
            string size = "0 Bytes";
            if (_totalSize >= 1073741824.0)
                size = String.Format("{0:##.##}", _totalSize / 1073741824.0) + " GB";
            else if (_totalSize >= 1048576.0)
                size = String.Format("{0:##.##}", _totalSize / 1048576.0) + " MB";
            else if (_totalSize >= 1024.0)
                size = String.Format("{0:##.##}", _totalSize / 1024.0) + " KB";
            else if (_totalSize > 0 && _totalSize < 1024.0)
                size = _totalSize.ToString() + " Bytes";
            Console.WriteLine(size);
        }

        public void PrintSize(double tempSize)
        {
            //Formatting bytesize to correct suffix.
            string size = "0 Bytes";
            if (tempSize >= 1073741824.0)
                size = String.Format("{0:##.##}", tempSize / 1073741824.0) + " GB";
            else if (tempSize >= 1048576.0)
                size = String.Format("{0:##.##}", tempSize / 1048576.0) + " MB";
            else if (tempSize >= 1024.0)
                size = String.Format("{0:##.##}", tempSize / 1024.0) + " KB";
            else if (tempSize > 0 && tempSize < 1024.0)
                size = tempSize.ToString() + " Bytes";
            Console.WriteLine(size);
        }

        public void PrintExcluded(string[] excluded)
        {
            if (excluded.Length == 0) return;
            Console.WriteLine("Following file(s) are not eligible for extraction:\n");
            foreach (var item in excluded)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Valid extensions: .zip, .rar");
        }

        public void PrintExtracting(string file)
        {
            Console.WriteLine($"Extracting: {file}");
        }

    }
}