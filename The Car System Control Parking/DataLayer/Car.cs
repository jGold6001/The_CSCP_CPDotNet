using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string VehicleID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(30)]
        public string VIN { get; set; }
        [Required]
        [MaxLength(25)]
        public string Color { get; set; }
        public ICollection<Place> Places { get; set; }
        public Car()
        {
            Places = new List<Place>();
        }
    }
}
