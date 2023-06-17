using DAL.Data.Context;
using DAL.Data.Models;
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

			var AllApartments = await _Context.Appartments.Include(a => a.Broker).Where(a => a.Type == type && a.Pending == false).ToListAsync();
			return AllApartments;

		}

		Appartment IApartmentRepo.GetApartmentDetails(int id)
		{
			return _Context.Appartments.Include(a => a.Broker).FirstOrDefault(a => a.Id == id && a.Pending == false);
		}
		public void AddToFavorite(string userId, int apartId)
		{
			var FavApartment = new UserApartement
			{
				UserId = userId,
				ApartementId = apartId,

			};
			_Context.UserAppartement.Add(FavApartment);
		}

		public IEnumerable<DAL.Data.Models.Appartment>? GetAddedToFavorite(string id)
		{
			var favapart = _Context.UserAppartement.Include(a => a.Appartment).ThenInclude(a => a.Broker).Where(a => a.UserId == id);
			var fav = favapart.Select(a => a.Appartment);
			return fav;
		}
    
    
        async Task<IEnumerable<Appartment>> IApartmentRepo.Search(string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice)
        {

            var result = await _Context.Appartments.Include(a=>a.Broker).ToListAsync();

            if (!string.IsNullOrEmpty(City)) { 
            
                result =  result.Where(a => a.City.Contains(City)).ToList();

            }
              
            if (!string.IsNullOrEmpty(Address)) {
    
              result = result.Where(a => a.Address.Contains(Address)).ToList();
            
            }

            if (minArea != 0)
            {
                result = result.Where(a => a.Area > minArea).ToList();

               }


            if (maxArea != 0)
            {
                result = result.Where(a => a.Area < maxArea).ToList();

            }


            if (maxPrice != 0) {


                result = result.Where(a => a.MaxPrice < maxPrice).ToList();

            }

            if (minPrice != 0)
            { 

                result = result.Where(a => a.MaxPrice > minPrice).ToList();

            }

            return result;

        }

		public int SaveChanges()
		{
			return _Context.SaveChanges();
		}
	}

}
