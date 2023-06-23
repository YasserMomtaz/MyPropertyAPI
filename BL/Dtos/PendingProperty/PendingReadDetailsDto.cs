using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.PendingProperty
{
    public class PendingReadDetailsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public int? MaxPrice { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }
        public int? Area { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DateTime? AdDate { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public string? Code { get; set; } = "";

        public string[] Photos { get; set; }
        public string? Username { get; internal set; }
    }
}
