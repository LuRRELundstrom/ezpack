using System;

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
            var fileOp = new FileOperations();
            var init = fileOp.InitialInput(args);
            if (init != null) fileOp.DetermineExtract(args);
        }
    }
}