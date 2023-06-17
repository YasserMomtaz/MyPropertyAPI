﻿using BL.Dtos;
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

        Task<IEnumerable<ApartmentList>> Search(string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice);
      
    }
}

