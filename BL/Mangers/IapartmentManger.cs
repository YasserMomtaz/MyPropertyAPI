using BL.Dtos;

namespace BL.Mangers
{
	public interface IapartmentManger
	{
		Task<IEnumerable<ApartmentList>> GetAll(string type);
		ApartmentDetails GetApartmentDetails(int id);

		IEnumerable<ApartmentList> GetAddedToFavorite(string id);
		void AddToFavorite(string userId, int apart);
	}
}
