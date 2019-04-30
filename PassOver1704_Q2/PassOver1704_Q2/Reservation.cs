using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    class Reservation
    {
        public int iD { get; set; }
        public int ClientNumber { get; set; }
        public int ProductNumber { get; set; }
        public int QuantityOfReservation { get; set; }
        public double TotalPriceOfReservation { get; set; }
        
        public Reservation()
        {

        }

        public override string ToString()
        {
            return $"Reservation is id: {iD}, Client Number: {ClientNumber}, Product Number {ProductNumber}, Quantity Of Reservation {QuantityOfReservation}, " +
                $"Total Price Of Reservation {TotalPriceOfReservation}";
        }
    }
}
