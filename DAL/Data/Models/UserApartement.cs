using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class UserApartement
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Appartment")]
        public int ApartementId { get; set; }

        public User User { get; set; }

        public Appartment Appartment { get; set; }
    }
}
