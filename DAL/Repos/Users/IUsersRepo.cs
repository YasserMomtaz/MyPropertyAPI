using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Users
{
    public interface IUsersRepo
    {
        void AddAppatrtement(Appartment NewAppartement);
        int SaveChanges();

    }
}
