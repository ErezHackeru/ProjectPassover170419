using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            Start:
            Console.WriteLine("Program for reservations");
            Console.WriteLine("==========================");
            Console.WriteLine("Choose your number:");
            Console.WriteLine();
            Console.WriteLine("1. Old CLient");
            Console.WriteLine("2. New Client");
            Console.WriteLine("3. Old Supplier");
            Console.WriteLine("4. New Supplier");
            Console.WriteLine("5. Print traceability table");
            Console.WriteLine("==========================");

            int ChosenNumber  = Convert.ToInt32(Console.ReadLine());

            switch (ChosenNumber)
            {
                case 1: ProgramLogicFunctions.ExistingClient(); goto Start;
                case 2: ProgramLogicFunctions.NewClient(); goto Start;
                case 3: ProgramLogicFunctions.ExistingSupplier(); goto Start;
                case 4: ProgramLogicFunctions.NewSupplier(); goto Start;
                case 5: ProgramLogicFunctions.PrintTraceabilityTable(); goto Start;
                default:break;
            }

            Console.ReadKey();
        }
    }
}
