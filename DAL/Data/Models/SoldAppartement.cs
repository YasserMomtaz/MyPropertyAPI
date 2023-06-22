using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class SoldAppartement
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }

        [ForeignKey("User"), Required]

        public string UserId { get; set; }

        [ForeignKey("Broker")]

        //make it null in db
        public string? BrokerId { get; set; }

        //make it null in db
        [ForeignKey("Admin")]
        public string? AdminId { get; set; }

        public int? Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? Area { get; set; }
        public string? Notes { get; set; }
        public string? Description { get; set; }
        public string? MiniDescription { get; set; }
        public string? Type { get; set; }

        public DateTime? AdDate { get; set; }
        public DateTime? SellingDate { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? ViewsCounter { get; set; }

        public User User { get; set; }
        public Broker Broker { get; set; }
        public Admin Admin { get; set; }

    }
}
