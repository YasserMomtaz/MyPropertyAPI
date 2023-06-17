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

     
       
        [HttpPost]
        public ActionResult<List<PhotoDto>> Upload(List<IFormFile> files)
        {

            #region Checking Extension

            //TODO: It's better to be part of appsettings.json
            var allowedExtenstions = new string[]
            {
        ".png",
        ".jpg",
        ".svg"
            };

            var photoDtos = new List<PhotoDto>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);

                bool isExtensionAllowed = allowedExtenstions.Contains(extension,
                    StringComparer.InvariantCultureIgnoreCase);
                if (!isExtensionAllowed)
                {
                    photoDtos.Add(new PhotoDto(false, "Extension is not valid"));
                    continue;
                }

                #endregion


                #region Checking Length

                bool isSizeAllowed = file.Length is > 0 and <= 4_000_000;
                if (!isSizeAllowed)
                {
                    photoDtos.Add(new PhotoDto(false, "Size is not allowed"));
                    continue;
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
                photoDtos.Add(new PhotoDto(true, "Success", url));

                #endregion
            }

            return photoDtos;

        }
    }
}
