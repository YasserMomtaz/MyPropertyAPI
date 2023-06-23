using BL.Dtos.PendingProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repos.PendingProperty;
using DAL.Data.Models;

namespace BL.Managers.PendingProperty
{ 
    public class PendingPropertyManager : IPendingPropertyManager
    {
        private readonly IPendingPropertyRepo _property;

        public PendingPropertyManager(IPendingPropertyRepo pendingProperty)
        {
            _property = pendingProperty;
        }
        public Appartment? Accept(int id, string brokerId, string managerId)
        {
            /*var propertyFromDB= _property.GetById(id);
            if (propertyFromDB == null) { return; }*/
          var apartment=  _property.Accept(id,brokerId,managerId);
            _property.SaveChanges();
            return apartment;
        }

        public void Delete(int id)
        {
           
            _property.Delete(id);
            _property.SaveChanges();

        }

        public List<PendingReadDto> GetAll()
        {
           var appartmentFromDb= _property.GetAll();
            if(appartmentFromDb == null) { return null; }
            return appartmentFromDb.Select(a=> new PendingReadDto
            {
                Id= a.Id,
                Title= a.Title,
                UserName=a.User.UserName
            }).ToList();
           
        }

        public List<BrokerDataDto> GetAllBroker()
        {

            IEnumerable<Broker> brokersFromDb = _property.GetAllBroker();
            return brokersFromDb.Select(a => new BrokerDataDto
            {
                BrokerId = a.Id,
                BrokerName = a.UserName,

            }).ToList();
        }

        public PendingReadDetailsDto? GetById(int id)
        {
           var appartment=_property.GetById(id);
            if (appartment == null)
            {
                return null;
            }
            return new PendingReadDetailsDto
            {
                Id = appartment.Id,
                Title = appartment.Title,
                MaxPrice= appartment.MaxPrice,
                Address= appartment.Address,
                AdDate= appartment.AdDate,
                City = appartment.City,
                Area = appartment.Area,
                Description = appartment.Description,
                Bedrooms = appartment.Bedrooms,
                Bathrooms = appartment.Bathrooms,
                Type = appartment.Type,
                Code = appartment.Code,
                Photos =appartment.Photos.Select(a=>a.PhotoUrl).ToArray(),
                Username = appartment.User.UserName
            };

            
        }
    }
}
