using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Apartment
{
    public interface IApartmentRepo
    {
         Task<IEnumerable<Appartment>> GetAll(string type);
        Appartment GetApartmentDetails(int id);

        Task<IEnumerable<Appartment>> Search(string city, string address, int minarea,int maxarea, int minprice, int maxprice);

    }
}
