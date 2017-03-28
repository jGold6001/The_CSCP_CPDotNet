using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{

    public class Record 
    {            
        public int NumberPLace { get; set; }
        public string ClientLastName { get; set; }
        public string CarBrand { get; set; }
        public DateTime DatePayment { get; set; }
        public decimal Rent { get; set; }
        public decimal Debt { get; set; }
    }

}
