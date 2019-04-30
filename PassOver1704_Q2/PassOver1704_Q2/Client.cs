using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    class Client
    {
        public int iD { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrivateName { get; set; }
        public string LastName { get; set; }
        public int CreditCardNumber { get; set; } //Last 4 digits

        public Client()
        {

        }

        public override string ToString()
        {
            return $"Client is id: {iD} User Name: {UserName}, Password {Password}, FirstName {PrivateName}, " +
                $"LastName {LastName}, CreditCardNumber {CreditCardNumber}";
        }
    }
}
