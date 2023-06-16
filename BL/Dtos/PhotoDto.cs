using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class PhotoDto
    {

        public int PhotoId { get; set; }
        public int ApartmentId { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


        public PhotoDto(int apartmentId, int photoId, string url = "")
        {
            ApartmentId = apartmentId;
            PhotoId= photoId;
            PhotoUrl = url;
        }

   
    }
}
