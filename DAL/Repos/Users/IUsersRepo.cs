using DAL.Data.Models;

namespace DAL.Repos.Users
{

    public interface IUsersRepo
    {
        void AddAppatrtement(Appartment NewAppartement, string[] photos);
        int SaveChanges();





	}
}
