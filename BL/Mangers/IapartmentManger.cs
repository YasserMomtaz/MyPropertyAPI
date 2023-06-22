using BL.Dtos;
using BL.Dtos.Apartment;

namespace BL.Mangers
{

	public interface IapartmentManger
	{
        Task<ApartmentListPaginationDto> GetAll(string type,int page ,int CountPerPage);
		ApartmentDetails GetApartmentDetails(int id);
		Task<ApartmentListPaginationDto> Search(int page, int CountPerPage, string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice,string type);
		IEnumerable<ApartmentList> GetAddedToFavorite(string id);
		void AddToFavorite(string userId, int apart);
	}

}

