using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
        public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [ForeignKey("Appartment")]
        public int ApartmentId { get; set; }
        public string PhotoUrl { get; set; }

        public Appartment Appartment { get; set; }
    }
}
