using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{
    public class Tariff
    {
        [Key]
        public int Id { get; set; }
        public int? RentValueId { get; set; }      
        public decimal Deposit { get; set; }  
        public decimal Debt { get; set; }
        public DateTime DatePayment { get; set; }
        public ICollection<Place> Places { get; set; }
        public RentValue RentValue { get; set; }
        public Tariff()
        {
            Places = new List<Place>();
        }

        public override string ToString()
        {
            return String.Format
                (
                    " Rental: {0}\n Cost: {1}\n Deposit: {2}\n DatePayment: {3}\n Debt: {4}\n", RentValue.Id, RentValue.Price, Deposit, DatePayment, Debt
                );
        }

    }

}
