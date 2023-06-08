using BL.Dtos.PendingProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repos.PendingProperty;

namespace BL.Managers.PendingProperty
{
    public class PendingPropertyManager : IPendingProperty
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


        }

        public void Delete(int id)
        {
            var propertyFromDB=_property.GetById(id);
            if (propertyFromDB == null) { return; }
            _property.Delete(propertyFromDB);
            _property.SaveChanges();

        }

        public List<PendingReadDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public PendingReadDetailsDto? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
