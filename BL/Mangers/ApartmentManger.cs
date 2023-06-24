using BL.Dtos;

using BL.Dtos.Apartment;

using BL.Dtos.PendingProperty;

using DAL.Data.Models;
using DAL.Migrations;
using DAL.Repos.Apartment;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace BL.Mangers
{

	public class ApartmentManger : IapartmentManger
	{
		IApartmentRepo _apartmentRepo;
		public ApartmentManger(IApartmentRepo apartmentRepo)
		{
			_apartmentRepo = apartmentRepo;
		}


		public async Task<ApartmentListPaginationDto> GetAll(string type,int page,int CountPerPage, string userId)
		{
			IEnumerable<Appartment> ApartmentDB = await _apartmentRepo.GetAll(type,page,CountPerPage);
			var fav = _apartmentRepo.GetUserApartments(userId);
			var apartmentList= ApartmentDB.Select(A => new ApartmentList
			{
				Id = A.Id,
				Title = A.Title,
				Area = A.Area,
				Bathrooms = A.Bathrooms,
				Bedrooms = A.Bathrooms,
				MiniDescription = A.MiniDescription,
				Address = A.Address,
				AdDate = A.AdDate,
				City = A.City,
				MaxPrice = A.MaxPrice,
				BrokerPhone = A.Broker.PhoneNumber,
				BrokerEmail = A.Broker.Email,
				Type = A.Type,
				photos = A.Photos.Select(a => a.PhotoUrl).ToArray(),
				IsFavorite = null != fav.FirstOrDefault(a=>a.Id== A.Id) ,
				
			}).ToList();
			var apartmentCount = _apartmentRepo.GetCount(type);
			return new ApartmentListPaginationDto { ApartmentList= apartmentList ,ApartmentCount =apartmentCount};

		}

		public async Task<ApartmentListPaginationDto> Search(int page, int CountPerPage,string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice, string type, string userId)
		{

			IEnumerable<Appartment> result = await _apartmentRepo.Search(page ,CountPerPage,City, Address, minArea, maxArea, minPrice, maxPrice,type);
            var fav = _apartmentRepo.GetUserApartments(userId);

            var SearchedItems= result.Select(A => new ApartmentList
			{


                Id = A.Id,
                Title = A.Title,
                Area = A.Area,
                Bathrooms = A.Bathrooms,
                Bedrooms = A.Bathrooms,
                MiniDescription = A.MiniDescription,
                Address = A.Address,
                AdDate = A.AdDate,
                City = A.City,
                MaxPrice = A.MaxPrice,
                BrokerPhone = A.Broker.PhoneNumber,
                BrokerEmail = A.Broker.Email,
			    Type=A.Type,
				photos=A.Photos.Select(a=>a.PhotoUrl).ToArray(),
                IsFavorite = null != fav.FirstOrDefault(a => a.Id == A.Id),

            }).ToList();


            var apartmentCount = _apartmentRepo.GetCountSearch(City, Address, minArea, maxArea, minPrice, maxPrice, type);
            return new ApartmentListPaginationDto { ApartmentList = SearchedItems, ApartmentCount = apartmentCount };

        }

        public ApartmentDetails GetApartmentDetails(int id, string userId)
		{
			Appartment ApartmentDB = _apartmentRepo.GetApartmentDetails(id);
            var fav = _apartmentRepo.GetUserApartments(userId);
			var favorite = fav.FirstOrDefault(a=>a.Id== id);
            return new ApartmentDetails
			{
				Id = ApartmentDB.Id,
				Title = ApartmentDB.Title,
				Area = ApartmentDB.Area,
				Bathrooms = ApartmentDB.Bathrooms,
				Bedrooms = ApartmentDB.Bathrooms,
				Description = ApartmentDB.Description,
				Address = ApartmentDB.Address,
				AdDate = ApartmentDB.AdDate,
				City = ApartmentDB.City,
				MaxPrice = ApartmentDB.MaxPrice,
				BrokerPhone = ApartmentDB.Broker.PhoneNumber,
				BrokerEmail = ApartmentDB.Broker.Email,
				Type = ApartmentDB.Type,
				ViewsCount = ApartmentDB.ViewsCounter.Value,
				Code= ApartmentDB.Code,
				Photos = ApartmentDB.Photos.Select(a => a.PhotoUrl).ToArray(),
				isFavorite=null!=favorite,
			};

		}

		public void AddToFavorite(string userId, int apartId)
		{

			var FavApartment = new UserApartement
			{
				UserId = userId,
				ApartementId = apartId,

			};
			_apartmentRepo.AddToFavorite(FavApartment.UserId, FavApartment.ApartementId);
			_apartmentRepo.SaveChanges();
		}
        public void RemoveFromFavorite(string userId, int apartId)
        {

            _apartmentRepo.RemoveFromFavorite(userId, apartId);
            _apartmentRepo.SaveChanges();
        }

        IEnumerable<ApartmentList> IapartmentManger.GetAddedToFavorite(string id)
		{
			IEnumerable<Appartment> ApartmentDB = _apartmentRepo.GetUserApartments(id);
            return ApartmentDB.Select(a => new ApartmentList
			{
				Id = a.Id,
				Title = a.Title,
				Area = a.Area,
				Bathrooms = a.Bathrooms,
				Bedrooms = a.Bathrooms,
				MiniDescription = a.MiniDescription,
				Address = a.Address,
				AdDate = a.AdDate,
				City = a.City,
				MaxPrice = a.MaxPrice,
				BrokerPhone = a.Broker.PhoneNumber,
				BrokerEmail = a.Broker.Email,
				photos=a.Photos.Select(a=>a.PhotoUrl).ToArray(),
				IsFavorite=true,
			}).ToList();


		}

		public int SellAppartement (SoldAppartementDto sellDto)
		{
			return _apartmentRepo.sellAppartement(sellDto.AppartementId,sellDto.Price); ;
		}
		public int DeleteAppartement(int Id)
		{
			return _apartmentRepo.DeleteAppartement(Id);
		}
	



        public async Task<IEnumerable<ApartmentList>> GetAppartmentsOfBroker(string brokerId)
        {


            IEnumerable<Appartment> result = await _apartmentRepo.GetAppartmentsOfBroker(brokerId);

			return result.Select(a => new ApartmentList
			{

				Id = a.Id,
				Title = a.Title,
				Area = a.Area,
				Bathrooms = a.Bathrooms,
				Bedrooms = a.Bathrooms,
				MiniDescription = a.MiniDescription,
				Address = a.Address,
				AdDate = a.AdDate,
				City = a.City,
				MaxPrice = a.MaxPrice,

                photos = a.Photos.Select(a => a.PhotoUrl).ToArray(),


            }).ToList();

        }
    

		public IEnumerable<ApartmentList> GetAllUserApartments(string id)
		{
			IEnumerable<Appartment> apartmentDB = _apartmentRepo.GetAllUserApartments(id);

			if (apartmentDB == null)
			{
				return null;
			}
			else
			{
				var apartmentList = apartmentDB.Select(a => new ApartmentList
				{
					Id = a.Id,
					Title = a.Title,
					Area = a.Area,
					Bathrooms = a.Bathrooms,
					Bedrooms = a.Bathrooms,
					MiniDescription = a.MiniDescription,
					Address = a.Address,
					AdDate = a.AdDate,
					City = a.City,
					MaxPrice = a.MaxPrice,
					photos = a.Photos.Select(p => p.PhotoUrl).ToArray()
				}).ToList();

				return apartmentList;
			}

		}
	}
}

