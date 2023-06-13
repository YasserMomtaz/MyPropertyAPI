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
        public void Accept(int id)
        {
            /*var propertyFromDB= _property.GetById(id);
            if (propertyFromDB == null) { return; }*/
            _property.Accept(id);
            _property.SaveChanges();

        }

        public void Delete(int id)
        {
           
            _property.Delete(id);
            _property.SaveChanges();

        }

        public List<PendingReadDto> GetAll()
        {
           IEnumerable<Appartment> appartmentFromDb= _property.GetAll();
            return appartmentFromDb.Select(a=> new PendingReadDto
            {
                Id= a.Id,
                Title= a.Title,
                UserName=a.User.UserName
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
                Username = appartment.User.UserName
            };
        }
    }
}
