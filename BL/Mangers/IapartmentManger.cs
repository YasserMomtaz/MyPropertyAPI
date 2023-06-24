using BL.Dtos;

using BL.Dtos.Apartment;

using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BL.Mangers
{

	public interface IapartmentManger
	{
        Task<ApartmentListPaginationDto> GetAll(string type,int page ,int CountPerPage,string userId);
		ApartmentDetails GetApartmentDetails(int id,string userId);

		int SellAppartement(SoldAppartementDto sellDto);
		int DeleteAppartement(int Id);

    


		IEnumerable<ApartmentList> GetAllUserApartments(string id);
	

		Task<ApartmentListPaginationDto> Search(int page, int CountPerPage, string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice,string type,string userId);
		IEnumerable<ApartmentList> GetAddedToFavorite(string id);
		void AddToFavorite(string userId, int apart);
		void RemoveFromFavorite(string userId, int apartId);
        Task<IEnumerable<ApartmentList>> GetAppartmentsOfBroker(string brokerId);


    }



}

