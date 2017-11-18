using System;
using System.IO;

class FileOperations
{

    public void AwaitInput(string[] args)
    {

        if (args.Length > 0 && File.Exists(args[0]))
        {
            string path = args[0];
            foreach (var item in args)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string _parentfolder = Directory.GetParent(item).Name;
                    Console.WriteLine(_parentfolder);

                }
            }
            Console.ReadKey();
        }

        else
        {
            Console.WriteLine("Awaiting input..");
        }
    }

}