using BL.Dtos;
using BL.Dtos.UserDtos;
using BL.Mangers.Users;
using DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyPropertyAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUersManger _UsersManger;
        private readonly UserManager<IdentityUser> UserManagerFromPackage;
        private readonly IConfiguration _configuration;

        public UsersController(IUersManger UsersManger, UserManager<IdentityUser> usermanger, IConfiguration configuration)
        {
            _UsersManger = UsersManger;
            UserManagerFromPackage = usermanger;
            _configuration = configuration;
        }

       // [Authorize]
        [HttpPost]
        [Route("AddAppartement")]
        public async Task<ActionResult> AddAppartement(SellingAppartementDto NewAppartement)
        {

            //var user = await UserManagerFromPackage.GetUserAsync(User);

            //NewAppartement.UserId = user.Id;

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
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Register(UserLoginDto userLoginDto)
        {
            var user = await UserManagerFromPackage.FindByNameAsync(userLoginDto.UserName);

            if (user == null)
            {
                return BadRequest("User Name or Password isn't Correct");
            }

            bool isPasswordCorrect = await UserManagerFromPackage.CheckPasswordAsync(user, userLoginDto.Password);
            if (!isPasswordCorrect)
            {

                return BadRequest("User Name or Password isn't Correct");
            }

            var claimsList = await UserManagerFromPackage.GetClaimsAsync(user);

            return GenerateToken(claimsList);

        }
        private TokenDto GenerateToken(IList<Claim> claimsList)
        {
            string keyString = _configuration.GetValue<string>("SecretKey") ?? string.Empty;
            var keyInBytes = Encoding.ASCII.GetBytes(keyString);
            var key = new SymmetricSecurityKey(keyInBytes);

            //Combination of secret Key and HashingAlgorithm
            var signingCredentials = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256Signature);

            //Putting All together
            var expiry = DateTime.Now.AddHours(3);

            var jwt = new JwtSecurityToken(
                    expires: expiry,
                    claims: claimsList,
                    signingCredentials: signingCredentials);

            //Getting Token String
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(jwt);

            return new TokenDto
            {
                Token = tokenString,

            };
        }
    }

}
