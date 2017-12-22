using System;
using System.IO;

/*
	AUTHOR: LuRRE


	DESCRIPTION:
    Creates new FileOp object and checks for input args before initializing unzip.


    PARAMETERS:


    RETURN

*/
namespace EZpack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EZpack by LuRRE\n===============\n");
            var init = new FileOperations();
            init.VerifyInput(args);
            init.ValidateFiles(init.VerifiedArgs);
            init.DetermineExtract(init.VerifiedArgs);
        }
    }
}