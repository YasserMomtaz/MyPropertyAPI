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


    public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }


        public PhotoDto(bool isSuccess, string message, string url = "")
        {
        
       
        IsSuccess = isSuccess;
        Message = message;
        URL = url;
        }

   
    }
}
