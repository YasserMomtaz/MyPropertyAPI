using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Searched
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
        public int? Bedrooms { get; set; }
        public int? Area { get; set; }
        public string? KeyWord { get; set; }

        public User? User { get; set; }
    }
}
