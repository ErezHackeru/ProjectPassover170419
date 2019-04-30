using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    static class ProgramLogicFunctions
    {
        static IClassDAO classDAO = new ClassDAO();

        public static void ExistingClient()
        {
            Console.WriteLine("Write your User Name: ");
            string UserName = Console.ReadLine();
            Console.WriteLine("Write your Password: ");
            string UserPassword = Console.ReadLine();
            int IsExist = classDAO.findClientUserAndPassword(UserName, UserPassword);
            if (IsExist != 0)
            {
                Console.WriteLine("Hello  Existing Client");
                Console.WriteLine("Your Name and password was correct!");
                Console.WriteLine("==========================");
                Console.WriteLine("Choose your number:");
                Console.WriteLine();
                Console.WriteLine("1. Reservation list + Total Price");
                Console.WriteLine("2. All Product list");
                Console.WriteLine("3. Make a new reservation.");
                Console.WriteLine("==========================");

                int YourChoise = Convert.ToInt32(Console.ReadLine());

                switch (YourChoise)
                {
                    case 1:
                        {
                            List<Reservation> AllReservations = classDAO.GetTheReservationList();
                            AllReservations.ForEach(e => Console.WriteLine(e.ToString()));
                            double Total = 0;
                            foreach (var item in AllReservations)
                            {
                                Total += item.TotalPriceOfReservation;
                            }
                            Console.WriteLine($"Tota amount of all reservations is: {Total}");
                            break;
                        }
                    case 2:
                        {
                            List<string> AllProducts = classDAO.GetAllProductList();
                            AllProducts.ForEach(e => Console.WriteLine(e.ToString()));
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Hello  Please make a new reservation:");                            
                            Console.WriteLine("==========================");
                            Console.WriteLine();
                            Console.WriteLine("Write the product name:");
                            string ProductName = Console.ReadLine();
                            bool productExist = classDAO.CheckProductExist(ProductName);
                            int ExisitingQuantityInStock = 100;
                            if ((productExist)&&(ExisitingQuantityInStock!=0))
                            {
                                Console.WriteLine("Write the Quantity to want:");
                                int QuantityToRes = Convert.ToInt32(Console.ReadLine());
                                if (QuantityToRes <= ExisitingQuantityInStock)
                                {                                    
                                    classDAO.MakeANewNRservation(UserName , UserPassword, ProductName, QuantityToRes);
                                }
                                else
                                    Console.WriteLine("Quantity you esked was more than we have in stock");
                            }
                            else if (ExisitingQuantityInStock == 0)
                                Console.WriteLine("Exisiting Quantity In Stock is 0 !");
                            else
                                Console.WriteLine("The Product does not exist");
                            break;
                        }
                    default:
                        break;
                }

            }
            else
                Console.WriteLine("Your Name and Passord was not Exist.");
            //make a new exceotion 
        }
                
        public static void NewClient()
        {
            Console.WriteLine("Hello  New Client");
            Console.WriteLine("Write your User Name: ");
            string UserName = Console.ReadLine();

            Console.WriteLine("Write your Password: ");
            string UserPassword = Console.ReadLine();

            Console.WriteLine("Write your Private Name: ");
            string PrivateName = Console.ReadLine();

            Console.WriteLine("Write your SurName: ");
            string SurName = Console.ReadLine();

            Console.WriteLine("Write your CreditCardNumber: ");
            int CreditCardNumber = Convert.ToInt32(Console.ReadLine());

            classDAO.AddUser(UserName, UserPassword, PrivateName, SurName, CreditCardNumber);
        }
        public static void ExistingSupplier()
        {
            Console.WriteLine("Hello  Existing Supplier");
            Console.WriteLine("Write your Supplier User Name: ");
            string SupplierName = Console.ReadLine();

            Console.WriteLine("Write your Supplier Password: ");
            string SupplierPassword = Console.ReadLine();

            int IsExist = classDAO.findSupplierUserAndPassword(SupplierName, SupplierPassword);

            if (IsExist != 0)
            {
                Console.WriteLine("Hello  Existing Supplier");
                Console.WriteLine("Your Name and password was correct!");
                Console.WriteLine("==========================");
                Console.WriteLine("Choose your number:");
                Console.WriteLine();
                Console.WriteLine("1. Add Product to the stock.");
                Console.WriteLine("2. All My Product list");
                Console.WriteLine("==========================");

                int YourChise = Convert.ToInt32(Console.ReadLine());

                switch (YourChise)
                {
                    case 1: AddProductToStock(SupplierName, SupplierPassword); break;
                    case 2:
                        {
                            List<string> AllProductsList = classDAO.GetAllProductList();
                            AllProductsList.ForEach(e => e.ToString());
                            break;
                        }
                    default:
                        break;
                }
            }
            else
                Console.WriteLine("Your Name and Passord was not Exist.");
        }

        private static void AddProductToStock(string SupplierName , string SupplierPassword)
        {
            Console.WriteLine("Please write the Product Name:");
            string NewProductName = Console.ReadLine();
            string OldSuplierName = classDAO.FindSuppllierFromProductName(NewProductName);
            if (OldSuplierName == string.Empty)
            {
                Console.WriteLine("Product Doesn't Exist.");
                Console.WriteLine("Please write All details about the product:");
                Console.WriteLine("1. Supplier Number:");
                int NewSupplierNumber = Convert.ToInt32(Console.ReadLine());                
                Console.WriteLine("2. Price of the product:");
                int NewPrice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("3. Quantity In Stock:");
                int NewQuantityInStock = Convert.ToInt32(Console.ReadLine());

                Product newProduct = new Product()
                {
                    ProductName = NewProductName,
                    SupplierNumber = NewSupplierNumber,
                    Price = NewPrice,
                    QuantityInStock = NewQuantityInStock
                };
                classDAO.AddProduct(newProduct);
            }
            else if (OldSuplierName == SupplierName)
            {
                Console.WriteLine("Supplier Exist. Please write the amount of products you want:");
                int AmountNeeded = Convert.ToInt32(Console.ReadLine());
                classDAO.AddAmountToStock(NewProductName, AmountNeeded);
            }
            else 
                Console.WriteLine("The product exists but from another supplier");
        }

        public static void NewSupplier()
        {
            Console.WriteLine("Hello  New Supplier");
            Console.WriteLine("Write your User Name: ");
            string UserName = Console.ReadLine();

            Console.WriteLine("Write your Password: ");
            string UserPassword = Console.ReadLine();

            Console.WriteLine("Write your Company Name: ");
            string CompanyName = Console.ReadLine();

            Console.WriteLine("Write your CreditCardNumber: ");
            int CreditCardNumber = Convert.ToInt32(Console.ReadLine());

            classDAO.AddSupplier(UserName, UserPassword, CompanyName);
        }
        public static void PrintTraceabilityTable()
        {
            Console.WriteLine("Traceability Table Below: ");
            Console.WriteLine("==========================");
            List<TraceabilityTable> traceabilityTables = classDAO.GetAllActions();

            traceabilityTables.ForEach(e => e.ToString());
        }
    }
}
