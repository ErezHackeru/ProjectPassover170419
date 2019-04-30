using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    class Product
    {
        public int iD { get; set; }
        public string ProductName { get; set; }
        public int SupplierNumber { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }
        
        public Product()
        {

        }

        public override string ToString()
        {
            return $"Product is id {iD} Product Name: {ProductName}, Supplier Number {SupplierNumber}, Price {Price}, " +
                $"QuantityInStock {QuantityInStock}";
        }
    }
}
