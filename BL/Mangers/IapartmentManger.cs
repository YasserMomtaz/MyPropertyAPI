using BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mangers
{
    public interface IapartmentManger
    {
        Task<IEnumerable<ApartmentList>> GetAll(string type);
        ApartmentDetails GetApartmentDetails(int id);
    }
}
