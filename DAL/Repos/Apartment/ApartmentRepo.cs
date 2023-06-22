using Azure;
using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repos.Apartment
{
	public class ApartmentRepo : IApartmentRepo
	{
		MyProperyContext _Context;
		public ApartmentRepo(MyProperyContext context)
		{
			_Context = context;
		}
		async Task<IEnumerable<Appartment>> IApartmentRepo.GetAll(string type,int page ,int CountPerPage)
		{


			var AllApartments = await _Context.Appartments
                                        .Include(a => a.Broker)
                                        .Where(a => a.Type == type && a.Pending == false)
                                        .Include(a=>a.Photos)
                                        .Skip(CountPerPage*(page-1)).Take(CountPerPage).ToListAsync();

			return AllApartments;

		}
        

		Appartment IApartmentRepo.GetApartmentDetails(int id)
		{
			var Appartement = _Context.Appartments.Include(a => a.Broker).Include(a => a.Photos).FirstOrDefault(a => a.Id == id && a.Pending == false);
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
			var FavApartment = new UserApartement
			{
				UserId = userId,
				ApartementId = apartId,

			};
			_Context.UserAppartement.Add(FavApartment);
		}

		public IEnumerable<DAL.Data.Models.Appartment>? GetUserApartments(string id)
		{
			var favapart = _Context.UserAppartement.Include(a => a.Appartment).ThenInclude(a => a.Broker).Where(a => a.UserId == id);
			var fav = favapart.Select(a => a.Appartment);
			return fav;
		}

    
    
        async Task<IEnumerable<Appartment>> IApartmentRepo.Search(int page, int CountPerPage,string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice, string type)
        {



		

			var result = await _Context.Appartments.Include(a => a.Broker).Where(a => a.Pending == false).Include(a => a.Photos).ToListAsync();
			var newSearched = new Searched();

			if (!string.IsNullOrEmpty(City))
			{

				result = result.Where(a => a.City.Contains(City)).ToList();
				newSearched.City = City;
			}

			if (!string.IsNullOrEmpty(Address))
			{

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


			if (maxPrice != 0)
			{


				result = result.Where(a => a.MaxPrice < maxPrice).ToList();
				newSearched.MaxPrice = maxPrice;

			}

			if (minPrice != 0)
			{

				result = result.Where(a => a.MaxPrice > minPrice).ToList();
				newSearched.MinPrice = minPrice;
			}


			_Context.Searched.Add(newSearched);
			_Context.SaveChanges();


			List<Appartment> reversed = result.Skip(CountPerPage*(page-1)).Take(CountPerPage).ToList();
			reversed.Reverse();


			return reversed;

		}

		public int SaveChanges()
		{
			return _Context.SaveChanges();
		}


		public IEnumerable<Appartment> GetAllUserApartments(string userId)
		{
			var userApartments = _Context.Appartments.Include(a => a.Photos).Where(a => a.UserId == userId);
			return userApartments;
		}
	}


       

        int IApartmentRepo.GetCount(string type)
        {
            var AllApartmentsCount =  _Context.Appartments.Where(a => a.Type == type && a.Pending == false).Count();
                                        
            return AllApartmentsCount;
        }

        int IApartmentRepo.GetCountSearch(string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice, string type)
        {
            var result =  _Context.Appartments.Include(a => a.Broker).Where(a => a.Pending == false).Include(a => a.Photos).ToList();

            if (!string.IsNullOrEmpty(City))
            {

                result = result.Where(a => a.City.Contains(City)).ToList();
            }

            if (!string.IsNullOrEmpty(Address))
            {
                result = result.Where(a => a.Address.Contains(Address)).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {

                result = result.Where(a => a.Type.Contains(type)).ToList();

            }

            if (minArea != 0)
            {
                result = result.Where(a => a.Area > minArea).ToList();
            }


            if (maxArea != 0)
            {
                result = result.Where(a => a.Area < maxArea).ToList();

            }


            if (maxPrice != 0)
            {


                result = result.Where(a => a.MaxPrice < maxPrice).ToList();

            }

            if (minPrice != 0)
            {

                result = result.Where(a => a.MaxPrice > minPrice).ToList();
            }


            int reversed = result.Count();
            return reversed;

      async  public Task<IEnumerable<Appartment>> GetAppartmentsOfBroker()
        {

           var result = await _Context.Appartments.Include(a=>a.Photos).Where(a => Convert.ToInt32(a.BrokerId) == 1).ToListAsync();


            return result;


        }
    }


}
