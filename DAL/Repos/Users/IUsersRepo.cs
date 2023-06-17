using DAL.Data.Models;

namespace DAL.Repos.Users
{
	public interface IUsersRepo
	{
		void AddAppatrtement(Appartment NewAppartement);
		int SaveChanges();




	}
}
