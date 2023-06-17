using DAL.Data.Context;
using DAL.Data.Models;

namespace DAL.Repos.Users
{
	public class UsersRepo : IUsersRepo
	{
		private readonly MyProperyContext Context;

		public UsersRepo(MyProperyContext context)
		{
			Context = context;
		}

		public void AddAppatrtement(Appartment NewAppartement)
		{

			Context.Appartments.Add(NewAppartement);
		}



		public int SaveChanges()
		{
			return Context.SaveChanges();
		}

	}
}
