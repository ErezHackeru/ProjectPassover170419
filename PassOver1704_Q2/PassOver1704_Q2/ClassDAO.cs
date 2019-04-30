using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    enum AllActionKind
    {
        WrongPasswordEnterTry,
        NewUserCreation,
        TryToProductPurchaseMoreThanInStock,
        ClientViewedProducts
    }

    class ClassDAO : IClassDAO
    {
        static SqlCommand cmd = new SqlCommand();

        static ClassDAO()
        {
            //Command and Data Reader            
            cmd.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=ReservationManagerDB;Integrated Security=True");
            cmd.Connection.Open();
        }
        public static void Close()
        {
            cmd.Connection.Close();
        }

        public void AddSupplier(string userName, string userPassword, string companyName)
        {
            //INSERT INTO Supplier(UserName, PassWord, CompanyName)
            //Values('Voodoo', 'Fork123', 'ForksAndSpoons')
            /*
             * Creating the Stored procedurein MSSQL (SSMS):
            CREATE PROCEDURE INSERT_INTO_Supplier @UserName1 nvarchar(50), @PassWord1 nvarchar(50), @CompanyName1 nvarchar(50)
            AS
            INSERT INTO Supplier(UserName, PassWord, CompanyName)
            Values(@UserName1, @PassWord1, @CompanyName1)
            */
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = $"INSERT_INTO_Supplier";
            cmd.Parameters.Add(new SqlParameter("@UserName1", userName));
            cmd.Parameters.Add(new SqlParameter("@PassWord1", userPassword));
            cmd.Parameters.Add(new SqlParameter("@CompanyName1", companyName));

            cmd.ExecuteNonQuery();
            UpdateTraceablitityTable(AllActionKind.NewUserCreation.ToString(), "Success");
        }

        public void AddUser(string userName, string userPassword, string privateName, string surName, int creditCardNumber)
        {
            //INSERT INTO Clients(UserName, Password, PrivateName, SurName, CreditCardNumber)
            //Values('Shlomii', 'Money123', 'Shlomi', 'Moaney', 9878)

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO Clients(UserName, Password, PrivateName, SurName, CreditCardNumber)"+
                $"Values('{userName}', '{userPassword}', '{privateName}', '{surName}', {creditCardNumber})";

            cmd.ExecuteNonQuery();
            UpdateTraceablitityTable(AllActionKind.NewUserCreation.ToString(), "Success");
        }
                
        public int findClientUserAndPassword(string userName, string userPassword)
        {
            //SELECT ID
            //FROM Clients
            //WHERE UserName = 'Jony' AND Password = 'NoNe'

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT ID " +
                                "FROM Clients " +
                                $"WHERE UserName = {userName} AND Password = {userPassword} "; 
            int ID_Clients = 0;
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    ID_Clients = (int)reader["ID"];
                }
            }

            if (ID_Clients != 0) return ID_Clients;
            else
            {
                UpdateTraceablitityTable(AllActionKind.WrongPasswordEnterTry.ToString(), "Fail");
                return 0;
            }

        }

        public int findSupplierUserAndPassword(string userName, string userPassword)
        {
            //SELECT ID
            //FROM Supplier
            //WHERE UserName = 'Sagi' AND Password = 'Insur'

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT ID " +
                                "FROM Supplier " +
                                $"WHERE UserName = {userName} AND Password = {userPassword} "; //'Sagi', 'Insur'
            int ID_Supplier = 0;
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    ID_Supplier = (int)reader["ID"];                    
                }
            }
            if (ID_Supplier != 0) return ID_Supplier;
            else
            {
                UpdateTraceablitityTable(AllActionKind.WrongPasswordEnterTry.ToString(), "Fail");
                return 0;
            }
        }

        public string FindSuppllierFromProductName(string productName)
        {
            //SELECT S.UserName, S.PassWord, S.CompanyName, P.ProductName
            //FROM Supplier S
            //INNER JOIN PRODUCTS P
            //ON S.ID = P.SupplierNumber
            //WHERE ProductName = 'Signature'

            //Result:
            //UserName PassWord CompanyName     ProductName
            //Sagi	   Insur	SagiInsurence	Signature
            //Then make an anonymous object that contains all .


            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT S.UserName, S.PassWord, S.CompanyName, P.ProductName"+
                        "FROM Supplier S INNER JOIN PRODUCTS P ON S.ID = P.SupplierNumber"+
                        $"WHERE ProductName = '{productName}' "; 
            
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    var ProductAndSupplierDetails = new
                    {
                        UserNameSupplier = (string)reader["UserName"],
                        PasswordSupplier = (string)reader["PassWord"],
                        CompanyNameSupplier = (string)reader["CompanyName"],
                        ProducName = (string)reader["ProductName"]
                    };

                    return ProductAndSupplierDetails.CompanyNameSupplier;
                }
            }


            return string.Empty;
        }

        public List<string> GetAllProductList()
        {
            //SELECT PRODUCTNAME
            //FROM Products

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT PRODUCTNAME FROM Products";
            List<string> ProductNames = new List<string>();

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        iD = (int)reader["ID"],
                        ProductName = (string)reader["ProductName"],
                        SupplierNumber = (int)reader["SupplierNumber"],
                        Price = (int)reader["Price"],
                        QuantityInStock = (int)reader["QuantityInStock"]
                    };
                    ProductNames.Add(product.ProductName);
                }
            }
            UpdateTraceablitityTable(AllActionKind.ClientViewedProducts.ToString(), "Success");
            return ProductNames;
        }

        public List<Reservation> GetTheReservationList()
        {
            //SELECT*
            //FROM Reservations

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM Reservations";            
            List<Reservation> ReserList = new List<Reservation>();

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    Reservation reservation = new Reservation()
                    {
                        iD = (int)reader["ID"],
                        ClientNumber = (int)reader["ClientNumber"],
                        ProductNumber = (int)reader["ProductNumber"],
                        QuantityOfReservation = (int)reader["QuantityReserved"],
                        TotalPriceOfReservation = (int)reader["TotalPriceOfResevation"]
                    };
                    ReserList.Add(reservation);
                }
                
            }
            UpdateTraceablitityTable(AllActionKind.ClientViewedProducts.ToString(), "Success");
            return ReserList;


        }

        public void MakeANewNRservation(string userName, string userPassword, string productName, int quantityToRes)
        {
            int ClientNumber = findClientUserAndPassword(userName, userPassword);
            int productNumber = getProductNumber(productName);
            int TotalPriceOfResevation = findTotalPriceOfResevationFromProductNameAndQuantity(productName, quantityToRes);
            //INSERT INTO Reservations(ClientNumber, ProductNumber, QuantityReserved, TotalPriceOfResevation)
            //VALUES(1, 1, 11, 1)

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO Reservations(ClientNumber, ProductNumber, QuantityReserved, TotalPriceOfResevation)" +
                $"Values({ClientNumber}, {productNumber}, quantityToRes, {TotalPriceOfResevation})";

            cmd.ExecuteNonQuery();
            UpdateTraceablitityTable(AllActionKind.NewUserCreation.ToString(), "Success");
        }

        private int findTotalPriceOfResevationFromProductNameAndQuantity(string productName, int quantityToRes)
        {
            //SELECT Products.ProductName, Products.Price
            //From Products
            //where ProductName = '...'

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Products.ProductName, Products.Price" +
                            $"FROM Products WHERE ProductName = '{productName}'";
            int TotalPrice = 0;

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    var productNPrice = new
                    {
                        ProductName = (string)reader["ProductName"],
                        ProductPrice = (int)reader["Price"]                        
                    };
                    TotalPrice = productNPrice.ProductPrice * quantityToRes;
                }

            }
            UpdateTraceablitityTable(AllActionKind.ClientViewedProducts.ToString(), "Success");
            
            return TotalPrice;
        }

        private int getProductNumber(string productName)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Products.ID, Products.ProductName" +
                            $"FROM Products WHERE ProductName = '{productName}'";
            int ProductID = 0;

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    ProductID = (int)reader["ID"];
                }
                else
                    ProductID = -1;
            }
            UpdateTraceablitityTable(AllActionKind.ClientViewedProducts.ToString(), "Success");

            return ProductID;
        }

        public bool CheckProductExist(string productName)
        {
            int ProductNumber = getProductNumber(productName);
            if (ProductNumber == -1)
                return false;
            else
                return true;
        }

        public void AddAmountToStock(string newProductName, int amountNeeded)
        {
            //UPDATE Products
            //SET QuantityInStock = 10
            //WHERE ProductName = 'Defence'
            
            int CurrentQuantity = findQuantityOfProduct(newProductName);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"UPDATE Products" +
                $"SET QuantityInStock = {CurrentQuantity+amountNeeded} WHERE ProductName = '{newProductName}'";

            cmd.ExecuteNonQuery();
        }

        private int findQuantityOfProduct(string newProductName)
        {
            //SELECT QuantityInStock
            //FROM Products
            //WHERE ProductName = 'Defence'

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT QuantityInStock" +
                            $"FROM Products WHERE ProductName = '{newProductName}'";
            int QuantityOfProduct = 0;

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    QuantityOfProduct = (int)reader["QuantityInStock"];
                }
                else
                    QuantityOfProduct = -1;
            }
            UpdateTraceablitityTable(AllActionKind.ClientViewedProducts.ToString(), "Success");

            return QuantityOfProduct;
        }

        public void AddProduct(Product newProduct)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO Product(ProductName, SupplierNumber, Price, QuantityInStock)" +
                $"Values('{newProduct.ProductName}', {newProduct.SupplierNumber}, {newProduct.Price}, {newProduct.QuantityInStock})";

            cmd.ExecuteNonQuery();
            UpdateTraceablitityTable(AllActionKind.NewUserCreation.ToString(), "Success");
        }

        public List<TraceabilityTable> GetAllActions() // All TraceabilityTable raws
        {
            List<TraceabilityTable> traceabilityTables = new List<TraceabilityTable>();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM TraceabilityTable";
            
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    TraceabilityTable TTable = new TraceabilityTable()
                    {
                        ActionNumber = (int)reader["ID"],
                        ActionDate = (DateTime)reader["DateAndTime"],
                        ActionKind = (string)reader["ActionKind"],
                        PassOrFail = (string)reader["SuccessFail"]
                    };
                    traceabilityTables.Add(TTable);
                }
            }
            UpdateTraceablitityTable(AllActionKind.ClientViewedProducts.ToString(), "Success");

            return traceabilityTables;
        }
        
        
        /// <summary>
        /// After every step in program
        /// </summary>
        private void UpdateTraceablitityTable(string ThisActionKind, string SuccessOrFail)
        {            
            string NowTime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO TraceabilityTable(DateAndTime, ActionKind, SuccessFail)" +
                $"Values({NowTime}, '{ThisActionKind}', '{SuccessOrFail}')";

            cmd.ExecuteNonQuery();
        }

        
    }
}
