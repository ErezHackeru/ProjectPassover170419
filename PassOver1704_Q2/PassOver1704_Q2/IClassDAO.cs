using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    interface IClassDAO
    {
        int findClientUserAndPassword(string userName, string userPassword);
        void AddUser(string userName, string userPassword, string privateName, string surName, int creditCardNumber);
        List<Reservation> GetTheReservationList();
        List<string> GetAllProductList();
        void MakeANewNRservation(string userName, string userPassword, string productName, int quantityToRes);
        int findSupplierUserAndPassword(string userName, string userPassword);        
        void AddSupplier(string userName, string userPassword, string companyName);
        string FindSuppllierFromProductName(string productName);
        bool CheckProductExist(string productName);
        void AddAmountToStock(string newProductName, int amountNeeded);
        void AddProduct(Product newProduct);
        List<TraceabilityTable> GetAllActions();
    }
}
