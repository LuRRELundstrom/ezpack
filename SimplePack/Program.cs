using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
	AUTHOR


	DESCRIPTION



    PARAMETERS


    RETURN

*/
namespace SimplePack
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("EZpack by LuRRE\n");
            FileOperations fo = new FileOperations();
            string init = fo.InitialInput(args);
            if (init != null) fo.Unrar(args);

        }


    }
}