using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6
{
    class Program
    {
        static void Main(string[] args)        
        {
            string path = "savefile.txt";
            Repository rep = new Repository(path);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            rep.Menu();            
        }
    }
}
