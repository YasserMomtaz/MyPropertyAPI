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


        async Task<IEnumerable<Appartment>> IApartmentRepo.Search(string city, string address, int minarea,int maxarea, int minprice, int maxprice)
        {

            var result = await _Context.Appartments.ToListAsync();

            if (city != null) { 
            
                result = await _Context.Appartments.Where(a => a.City.Contains(city)).ToListAsync();

            }
              
            if (address != null) {
    
              result = await _Context.Appartments.Where(a => a.Address.Contains(address)).ToListAsync();
            
            }

            if (minarea != null)
            {
                result = await _Context.Appartments.Where(a => a.Area > minarea).ToListAsync();

               }


            if (maxarea != null)
            {
                result = await _Context.Appartments.Where(a => a.Area < maxarea).ToListAsync();

            }


            if (maxprice != null) {


                result = await _Context.Appartments.Where(a => a.MaxPrice < maxprice).ToListAsync();

            }

            if (minprice != null)
            { 

                result = await _Context.Appartments.Where(a => a.MaxPrice > minprice).ToListAsync();

            }

            return result;


        }
    }
}
