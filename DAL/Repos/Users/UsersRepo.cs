using DAL.Data.Context;
using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
