using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Client
    {
        [Key]        
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string LastName {get;set;}
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(9)]
        public string PassportID { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime DateRegistred { get; set; }
        [Column(TypeName = "text")]
        public string AdditionalInfo { get; set; }
    
        public ICollection<Place> Places{ get; set; }
        public Client()
        {
            Places = new List<Place>();
        }

    }
}
