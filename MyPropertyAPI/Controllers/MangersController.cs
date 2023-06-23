using BL.Dtos.UserDtos;
using BL.Mangers.Users;
using DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyPropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   /* [Authorize(Policy = "Admin")]*/
    public class MangersController : ControllerBase
    {
        private readonly IUersManger _UsersManger;
        private readonly UserManager<IdentityUser> UserManagerFromPackage;
        private readonly IConfiguration _configuration;

        public MangersController(IUersManger UsersManger, UserManager<IdentityUser> usermanger, IConfiguration configuration)
        {
            _UsersManger = UsersManger;
            UserManagerFromPackage = usermanger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddBroker")]
        public async Task<ActionResult> AddBroker(UserRegisterDto RegisterDto)
        {
            var NewBroker = new Broker
            {
                Email = RegisterDto.Email,
                UserName = RegisterDto.UserName,
                PhoneNumber = RegisterDto.PhoneNumber,

            };
            var CreationResult = await UserManagerFromPackage.CreateAsync(NewBroker, RegisterDto.Password);
            if (!CreationResult.Succeeded)
            {
                return BadRequest(CreationResult.Errors);
            }

            var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, NewBroker.Id),
            new Claim(ClaimTypes.Role, "Broker"),
            new Claim("City", RegisterDto.City)
        };

            await UserManagerFromPackage.AddClaimsAsync(NewBroker, Claims);

            return NoContent();

        }
        [HttpPost]
        [Route("AddManger")]
        public async Task<ActionResult> AddMangers(UserRegisterDto RegisterDto)
        {
            var NewAdmin = new Admin
            {
                Email = RegisterDto.Email,
                UserName = RegisterDto.UserName,
                PhoneNumber = RegisterDto.PhoneNumber,

            };
            var CreationResult = await UserManagerFromPackage.CreateAsync(NewAdmin, RegisterDto.Password);
            if (!CreationResult.Succeeded)
            {
                return BadRequest(CreationResult.Errors);
            }

            var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, NewAdmin.Id),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("City", RegisterDto.City)
        };

            await UserManagerFromPackage.AddClaimsAsync(NewAdmin, Claims);

            return NoContent();

        }

    }
}
