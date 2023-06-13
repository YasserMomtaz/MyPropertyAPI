using BL.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mangers.Users
{
    public interface IUersManger
    {
        public void AddAppartement(SellingAppartementDto Appartementdto);
    }
}
