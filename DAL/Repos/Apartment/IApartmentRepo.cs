using DAL.Data.Models;

namespace DAL.Repos.Apartment
{

	public interface IApartmentRepo
	{
		Task<IEnumerable<Appartment>> GetAll(string type,int page , int CountpPage);
		Appartment GetApartmentDetails(int id);
		/////////////////
		IEnumerable<Appartment> GetAddedToFavorite(string id);
		void AddToFavorite(string userId, int apartId);
		int SaveChanges();
		int GetCount(string type);
        int GetCountSearch(string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice, string type);
        Task<IEnumerable<Appartment>> Search(int page, int CountPerPage,string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice,string type);


		Task<IEnumerable<Appartment>> GetAppartmentsOfBroker();


	}

}
