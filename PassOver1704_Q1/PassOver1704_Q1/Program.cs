using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            ///The table name is: PassOverDB
            
            Console.WriteLine("Hello To My Computer Game!");
            Console.WriteLine("=============================");
            
            int X1 = 0, Y1 = 0;
            DAO_Class dAO_Class = new DAO_Class();

            do
            {
                Console.WriteLine("Please write your X:");
                X1 = Convert.ToInt32(Console.ReadLine());
                if (X1 > 0)
                    dAO_Class.AddANumberToX(X1);
                Console.WriteLine("===============================");

                Console.WriteLine("Please write your Y:");                
                Y1 = Convert.ToInt32(Console.ReadLine());
                if (Y1 > 0)
                    dAO_Class.AddANumberToY(Y1);
                Console.WriteLine("===============================");
            }
            while ((X1 > 0) && (Y1 > 0));

            //dAO_Class.UpdateTheResultsTable();
            dAO_Class.printTheResults();
            Console.WriteLine("===============================");
            Console.WriteLine("===============================");
            Console.ReadKey();

            //dAO_Class.UpdateTheResults();
            //dAO_Class.printTheResults();
            DAO_Class.Close();
            Console.WriteLine("===============================");
            Console.WriteLine("The End !!");
            Console.ReadKey();
        }
    }
}
