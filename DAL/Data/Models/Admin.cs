using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Admin:IdentityUser
    {
        public ICollection<Appartment>? Appartment { get; set; }
        public ICollection<SoldAppartement> SoldAppartment { get; set; }


    }
}
