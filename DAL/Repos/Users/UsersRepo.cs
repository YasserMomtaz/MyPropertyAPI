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

        public void AddAppatrtement(Appartment NewAppartement, string[] photos)
        {
            
           Context.Appartments.Add(NewAppartement);
            SaveChanges();
            NewAppartement.Code = $"##DF{NewAppartement.Id}##";
            foreach (var item in photos)
            {
                var appartementPhoto = new Photo { ApartmentId = NewAppartement.Id, PhotoUrl = item };
                Context.Photo.Add(appartementPhoto);
            }
            SaveChanges();
        }
        public int  SaveChanges()
        {
            return Context.SaveChanges();
        }
    }

}
