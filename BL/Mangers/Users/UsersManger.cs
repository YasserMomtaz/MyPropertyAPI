using BL.Dtos.UserDtos;
using DAL.Data.Models;
using DAL.Repos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mangers.Users
{
    public class UsersManger :IUersManger
    {
        private readonly IUsersRepo  _UsersRepo;
        public UsersManger (IUsersRepo UsersRepo)
        {
            _UsersRepo= UsersRepo;
        }

        public void AddAppartement(SellingAppartementDto Appartementdto)
        {
            
            var NewAppartement = new Appartment
            {
                UserId= Appartementdto.UserId,
                Pending = true,
                Title = Appartementdto.Title,
                Address= Appartementdto.Address,
                City= Appartementdto.City,
                Area= Appartementdto.Area,
                Description= Appartementdto.Description,
                MiniDescription= Appartementdto.MiniDescription,
                MinPrice= Appartementdto.MinPrice,
                MaxPrice= Appartementdto.MaxPrice,
                Type= Appartementdto.Type,
                AdDate= Appartementdto.AdDate,
                Bedrooms= Appartementdto.Bedrooms,
                Bathrooms=Appartementdto.Bedrooms,
            };
            var photos = Appartementdto.PhotoUrl;

            _UsersRepo.AddAppatrtement(NewAppartement,photos);
            
            

        }

    }
}
