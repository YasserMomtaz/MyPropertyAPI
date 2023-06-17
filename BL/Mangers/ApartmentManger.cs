using BL.Dtos;
using DAL.Data.Models;
using DAL.Repos.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mangers
{
    public class ApartmentManger:IapartmentManger
    {
        IApartmentRepo _apartmentRepo;
        public ApartmentManger(IApartmentRepo apartmentRepo)
        {
            _apartmentRepo = apartmentRepo; 
        }


        public async Task<IEnumerable<ApartmentList>> Search(string City, string Address, int minArea,int maxArea, int minPrice, int maxPrice)
        {
            IEnumerable<Appartment> result = await _apartmentRepo.Search(City, Address, minArea, maxArea, minPrice, maxPrice);

            return result.Select(a => new ApartmentList {
            
            City = a.City,
            Address = a.Address,
            Area = a.Area,
            MaxPrice = a.MaxPrice, 
            
            }).ToList();



           
        }
        public async Task<IEnumerable<ApartmentList>> GetAll(string type)
        {
            IEnumerable<Appartment> ApartmentDB =await _apartmentRepo.GetAll(type);
            return ApartmentDB.Select(A => new ApartmentList
            {
                Id = A.Id,
                Title = A.Title,
                Area = A.Area,
                Bathrooms = A.Bathrooms,
                Bedrooms = A.Bathrooms,
                MiniDescription = A.MiniDescription,
                Address = A.Address,
                AdDate = A.AdDate,
                City = A.City,
                MaxPrice = A.MaxPrice,
                BrokerPhone =A.Broker.PhoneNumber,
                BrokerEmail = A.Broker.Email,
            }).ToList() ;
            
        }

         public ApartmentDetails GetApartmentDetails(int id)
        {
            Appartment ApartmentDB=_apartmentRepo.GetApartmentDetails(id);

            return new ApartmentDetails
            {
                Id = ApartmentDB.Id,
                Title = ApartmentDB.Title,
                Area = ApartmentDB.Area,
                Bathrooms = ApartmentDB.Bathrooms,
                Bedrooms = ApartmentDB.Bathrooms,
                Description = ApartmentDB.MiniDescription,
                Address = ApartmentDB.Address,
                AdDate = ApartmentDB.AdDate,
                City = ApartmentDB.City,
                MaxPrice = ApartmentDB.MaxPrice,
                BrokerPhone = ApartmentDB.Broker.PhoneNumber,
                BrokerEmail = ApartmentDB.Broker.Email,
                Type = ApartmentDB.Type

            };
            
        }

     
    }
}
