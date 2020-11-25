using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern_Project
{
    class Program
    {
        /* This main method is the entry to the application. It's the first thing to fire off when
        we run the program. All we want to do here is make our UI and run it.*/
        static void Main(string[] args)
        {
            ProgramUI program = new ProgramUI();
            program.Run();
        }
    }
}