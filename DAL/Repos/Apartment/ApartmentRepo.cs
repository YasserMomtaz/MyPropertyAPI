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

		public int SaveChanges()
		{
			return _Context.SaveChanges();
		}
	}
}
