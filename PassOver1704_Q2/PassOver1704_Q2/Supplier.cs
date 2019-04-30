using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    class Supplier
    {
        public int SupplierNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        
        public Supplier()
        {

        }

        public override string ToString()
        {
            return $"Supplier is Supplier Number: {SupplierNumber}, User Name: {UserName}, "+
                $"Password {Password}, CompanyName {CompanyName}";
        }
    }
}
