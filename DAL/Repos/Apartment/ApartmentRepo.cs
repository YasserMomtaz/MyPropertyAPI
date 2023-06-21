using DAL.Data.Context;
using DAL.Data.Models;
using DAL.Migrations;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repos.Apartment
{
	public class ApartmentRepo : IApartmentRepo
	{
		MyProperyContext _Context;
		public ApartmentRepo(MyProperyContext context)
		{
			_Context = context;
		}
		async Task<IEnumerable<Appartment>> IApartmentRepo.GetAll(string type)
		{

			var AllApartments = await _Context.Appartments.Include(a => a.Broker).Where(a => a.Type == type && a.Pending == false).Include(a=>a.Photos).ToListAsync();
			return AllApartments;

		}

		Appartment IApartmentRepo.GetApartmentDetails(int id)
		{
            var Appartement= _Context.Appartments.Include(a => a.Broker).Include(a => a.Photos).FirstOrDefault(a => a.Id == id && a.Pending == false);
            if (Appartement.ViewsCounter == null)
            {
                Appartement.ViewsCounter = 1;
            }
            else
            {
                Appartement.ViewsCounter += 1;
            }

            SaveChanges();
            return Appartement;
		}
		public void AddToFavorite(string userId, int apartId)
		{
            var userapartement=_Context.UserAppartement.FirstOrDefault(a=>a.UserId == userId && a.ApartementId==apartId);
            if(userapartement == null)
            {
                var FavApartment = new UserApartement
                {
                    UserId = userId,
                    ApartementId = apartId,

                };
                _Context.UserAppartement.Add(FavApartment);

            }

		}

		public IEnumerable<DAL.Data.Models.Appartment>? GetAddedToFavorite(string id)
		{
			var favapart = _Context.UserAppartement.Include(a => a.Appartment).ThenInclude(a => a.Broker).Where(a => a.UserId == id);
			var fav = favapart.Select(a => a.Appartment);
			return fav;
		}
    
    
        async Task<IEnumerable<Appartment>> IApartmentRepo.Search(string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice, string type)
        {

            var result = await _Context.Appartments.Include(a=>a.Broker).Where(a=>a.Pending==false).Include(a=>a.Photos).ToListAsync();
            var newSearched = new Searched();
           
            if (!string.IsNullOrEmpty(City)) { 
            
                result =  result.Where(a => a.City.Contains(City)).ToList();
                newSearched.City = City;   
            }
              
            if (!string.IsNullOrEmpty(Address)) {
    
              result = result.Where(a => a.Address.Contains(Address)).ToList();
              newSearched.Address = Address;
            }
            if (!string.IsNullOrEmpty(type))
            {

                result = result.Where(a => a.Type.Contains(type)).ToList();

            }

            if (minArea != 0)
            {
                result = result.Where(a => a.Area > minArea).ToList();
                newSearched.MinPrice = minPrice;
               }


            if (maxArea != 0)
            {
                result = result.Where(a => a.Area < maxArea).ToList();

            }


            if (maxPrice != 0) {


                result = result.Where(a => a.MaxPrice < maxPrice).ToList();
                newSearched.MaxPrice = maxPrice;

            }

            if (minPrice != 0)
            { 

                result = result.Where(a => a.MaxPrice > minPrice).ToList();
                newSearched.MinPrice= minPrice;
            }

            _Context.Searched.Add(newSearched);
            _Context.SaveChanges();

            List<Appartment> reversed = result.ToList();
            reversed.Reverse();


            return reversed;

        }
        public int sellAppartement(int Id,int soldPrice)
        {
            var appartement=_Context.Appartments.FirstOrDefault(a => a.Id == Id);
            if (appartement != null)
            {
                SoldAppartement SoldAppartement = new SoldAppartement
                {
                    Title = appartement.Title,
                    UserId = appartement.UserId,
                    BrokerId = appartement.BrokerId,
                    AdminId = appartement.AdminId,
                    Price = soldPrice,
                    Address = appartement.Address,
                    City = appartement.City,
                    Area = appartement.Area,
                    Notes = appartement.Notes,
                    Description = appartement.Description,
                    MiniDescription = appartement.MiniDescription,
                    Type = appartement.Type,
                    AdDate = appartement.AdDate,
                    SellingDate = DateTime.Now,
                    Bedrooms= appartement.Bedrooms,
                    Bathrooms=appartement.Bathrooms,
                    ViewsCounter = appartement.ViewsCounter,
                };
                _Context.Add(SoldAppartement);

                //deleting photos and favourites
                var photos=appartement.Photos;
                var Favourite = appartement.UserApartement;
                if(photos.Count!=0 )
                {
                    _Context.Remove(photos);
                }
                
                if(Favourite.Count!=0)
                {
                    _Context.Remove(Favourite);
                }
                
                _Context.Remove(appartement);
                _Context.SaveChanges();
                return 1;
            }else
            {
                return -1;
            }
        }

		public int SaveChanges()
		{
			return _Context.SaveChanges();
		}
	}

}
