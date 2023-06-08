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

        public void AddAppatrtement(Appartment NewAppartement)
        {
            
           Context.Appartments.Add(NewAppartement);
        }
        public int  SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
