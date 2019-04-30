using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q2
{
    class TraceabilityTable
    {
        public int ActionNumber { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionKind { get; set; }
        public string PassOrFail { get; set; }
        
        public TraceabilityTable()
        {

        }

        public override string ToString()
        {
            return $"TraceabilityTable is Action Number: {ActionNumber}, Action Date {ActionDate.ToString()},  " +
                $"Action Kind {ActionKind}, Pass Or Fail {PassOrFail}";
        }
    }
}
