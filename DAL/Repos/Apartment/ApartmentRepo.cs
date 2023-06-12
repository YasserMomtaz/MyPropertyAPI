using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Apartment
{
    public class ApartmentRepo : IApartmentRepo
    {
        MyProperyContext _Context;
        public ApartmentRepo(MyProperyContext context)
        {
            _Context= context;
        }
        async Task<IEnumerable<Appartment>> IApartmentRepo.GetAll(string type)
        {
            
              var  AllApartments =await _Context.Appartments.Include(a=>a.Broker).Where(a=>a.Type==type && a.Pending == false).ToListAsync();
            return AllApartments;
            
        }

        Appartment IApartmentRepo.GetApartmentDetails(int id)
        {
            return _Context.Appartments.Include(a=>a.Broker).FirstOrDefault(a => a.Id == id && a.Pending==false);
        }
    }
}
