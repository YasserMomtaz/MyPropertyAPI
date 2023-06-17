using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.UserDtos
{
    public class SellingAppartementDto
    {
        public string UserId { get; set; }
        public string Title { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        public int Area { get; set; }
        public string Description { get; set; } // wil be shown in the card

        public string MiniDescription { get; set; }
        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }
        public string Type { get; set; } // Rent or buy

        public DateTime AdDate { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }

        public string[] PhotoUrl { get; set; }
    }
}
