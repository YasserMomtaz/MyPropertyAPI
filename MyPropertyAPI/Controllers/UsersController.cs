using BL.Dtos.UserDtos;
using BL.Mangers.Users;
using DAL.Data.Models;
using DAL.Repos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyPropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUersManger _UsersManger;
        private readonly UserManager<User> UserManagerFromPackage;
        private readonly IConfiguration _configuration;

        public UsersController(IUersManger UsersManger, UserManager<User> usermanger, IConfiguration configuration)
        {
            _UsersManger = UsersManger;
            UserManagerFromPackage = usermanger;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddAppartement")]

        public async Task<ActionResult> AddAppartement(SellingAppartementDto NewAppartement)
        {
            //if (NewAppartement.UserId != null)
            //{
            //    return BadRequest("UserId isn't Required");
            //}

            //var _User = await UserManagerFromPackage.GetUserAsync(User);

            //NewAppartement.UserId = _User.Id;

            _UsersManger.AddAppartement(NewAppartement);

            return Ok();

                
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UserRegisterDto RegisterDto)
        {
            var NewUser = new User
            {
                Email = RegisterDto.Email,
                UserName = RegisterDto.UserName,
                PhoneNumber = RegisterDto.PhoneNumber,

            };
            var CreationResult = await UserManagerFromPackage.CreateAsync(NewUser, RegisterDto.Password);
            if (!CreationResult.Succeeded)
            {
                return BadRequest(CreationResult.Errors);
            }

            var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, NewUser.Id),
            new Claim(ClaimTypes.Role, "User"),
            new Claim("City", RegisterDto.City)
        };

            await UserManagerFromPackage.AddClaimsAsync(NewUser, Claims);

            return NoContent();






        }
    }
}
