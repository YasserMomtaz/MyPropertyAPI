using BL.Dtos;

namespace BL.Mangers
{

	public interface IapartmentManger
	{
		Task<IEnumerable<ApartmentList>> GetAll(string type);
		ApartmentDetails GetApartmentDetails(int id);
		Task<IEnumerable<ApartmentList>> Search(string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice,string type);
		IEnumerable<ApartmentList> GetAddedToFavorite(string id);
		void AddToFavorite(string userId, int apart);
		int SellAppartement(SoldAppartementDto sellDto);
		int DeleteAppartement(int Id);

    }

}

