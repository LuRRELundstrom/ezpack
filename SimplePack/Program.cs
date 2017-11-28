using System;
using System.IO;

/*
	AUTHOR: LuRRE


	DESCRIPTION:
    Creates new FileOp object and checks for input args before initializing unzip.


    PARAMETERS:


    RETURN

*/
namespace SimplePack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EZpack by LuRRE\n");
            var init = new FileOperations();            
            init.CheckInput(args);
            init.ValidateFiles(args);
            init.DetermineExtract(args);
        }
    }
}