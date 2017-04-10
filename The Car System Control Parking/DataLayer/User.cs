using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25)]

        public string Login { get; set; }
        [Required]
        [MaxLength(16)]
        public string Password { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
    }
}
