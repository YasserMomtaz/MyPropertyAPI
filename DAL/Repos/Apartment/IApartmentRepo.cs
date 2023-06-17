using DAL.Data.Models;

namespace DAL.Repos.Apartment
{

	public interface IApartmentRepo
	{
		Task<IEnumerable<Appartment>> GetAll(string type);
		Appartment GetApartmentDetails(int id);
		/////////////////
		IEnumerable<Appartment> GetAddedToFavorite(string id);
		void AddToFavorite(string userId, int apartId);
		int SaveChanges();
  Task<IEnumerable<Appartment>> Search(string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice);

	}

}
