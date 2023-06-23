using DAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BL.Dtos
{
	public class ApartmentList
	{
		[Key]
		public int Id { get; set; }
		public string? Title { get; set; }

		public string? BrokerPhone { get; set; }

		public string? BrokerEmail { get; set; }


        public int? MaxPrice { get; set; }
      
        public string? Address { get; set; }


		public string? City { get; set; }
		public int? Area { get; set; }
		public string? MiniDescription { get; set; }
        public string? Type { get; set; }
        public DateTime? AdDate { get; set; }
		public int? Bedrooms { get; set; }
		public int? Bathrooms { get; set; }
		public bool? IsFavorite { get; set; }
		public string[] photos { get; set; }


    }

}
