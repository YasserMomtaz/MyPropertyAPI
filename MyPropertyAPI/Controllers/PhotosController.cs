using DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;
using BL.Dtos;
using DAL.Data.Context;

namespace MyPropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : Controller
    {

        private readonly MyProperyContext _dbContext;

        public PhotosController(MyProperyContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpPost]
        public ActionResult<PhotoDto> Upload(IFormFile file ,int apartmentId )
        {

            #region Checking Extension

            var extension = Path.GetExtension(file.FileName);

            //TODO: It's better to be part of appsettings.json
            var allowedExtenstions = new string[]
            {
            ".png",
            ".jpg",
            ".svg"
            };

            bool isExtensionAllowed = allowedExtenstions.Contains(extension,
                StringComparer.InvariantCultureIgnoreCase);
            if (!isExtensionAllowed)
            {
                return BadRequest("Extension is not valid");
            }

            #endregion


            #region Checking Length

            bool isSizeAllowed = file.Length is > 0 and <= 4_000_000;
            //bool isSizeAllowed = file.Length > 0 && file.Length <= 4_000_000;
            if (!isSizeAllowed)
            {
                return BadRequest("Size is not allowed");
            }

            #endregion

            #region Storing The Image

            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Environment.CurrentDirectory, "Photos");
            var fullFilePath = Path.Combine(imagesPath, newFileName);

            using var stream = new FileStream(fullFilePath, FileMode.Create);
            file.CopyTo(stream);

            #endregion

            #region Generating URL

            var url = $"{Request.Scheme}://{Request.Host}/Photos/{newFileName}";


            var photo = new Photo
            {
                ApartmentId = apartmentId,
                PhotoUrl = url
            };

            _dbContext.Photo.Add(photo);
            _dbContext.SaveChanges();

            var photoDto = new PhotoDto(apartmentId, photo.PhotoId, url);

            return photoDto;
            #endregion



        }
    }
}
