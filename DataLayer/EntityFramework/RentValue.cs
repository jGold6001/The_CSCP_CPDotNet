using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RentValue
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }       
        public ICollection<Tariff> Tariffs { get; set; }
        public RentValue()
        {
            Tariffs = new List<Tariff>();
        }
    }
}
