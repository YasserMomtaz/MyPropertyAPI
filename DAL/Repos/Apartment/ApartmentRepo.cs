using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
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


        async Task<IEnumerable<Appartment>> IApartmentRepo.Search(string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice)
        {

            var result = await _Context.Appartments.ToListAsync();

            if (City != null) { 
            
                result = await _Context.Appartments.Where(a => a.City.Contains(City)).ToListAsync();

            }
              
            if (Address != null) {
    
              result = await _Context.Appartments.Where(a => a.Address.Contains(Address)).ToListAsync();
            
            }

            if (minArea != null)
            {
                result = await _Context.Appartments.Where(a => a.Area > minArea).ToListAsync();

               }


            if (maxArea != null)
            {
                result = await _Context.Appartments.Where(a => a.Area < maxArea).ToListAsync();

            }


            if (maxPrice != null) {


                result = await _Context.Appartments.Where(a => a.MaxPrice < maxPrice).ToListAsync();

            }

            if (minPrice != null)
            { 

                result = await _Context.Appartments.Where(a => a.MaxPrice > minPrice).ToListAsync();

            }

            return result;


        }
    }
}
