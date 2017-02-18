using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tarriff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Rental { get; set; }
        [Column(TypeName = "Money")]
        public decimal Cost { get; set; }
        public decimal Deposit { get; set; }
        public decimal Debt { get; set; }
        public DateTime DatePayment { get; set; }
        public ICollection<Place> Places { get; set; }
        public Tarriff()
        {
            Places = new List<Place>();
        }

        public override string ToString()
        {
            return String.Format
                (
                    " Rental: {0}\n Cost: {1}\n Deposit: {2}\n DatePayment: {3}\n Debt: {4}\n", Rental, Cost, Deposit, DatePayment, Debt
                );
        }

    }

}
