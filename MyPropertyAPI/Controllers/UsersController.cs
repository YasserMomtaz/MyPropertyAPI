using BL.Dtos.UserDtos;
using BL.Mangers.Users;
using DAL.Data.Models;
using DAL.Repos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyPropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUersManger _UsersManger;
        private readonly UserManager<User> UserManager;
        public UsersController(IUersManger UsersManger /*UserManager<User> usermanger*/)
        {
           _UsersManger= UsersManger;
/*            UserManager= usermanger;
*/        }
        [HttpPost]
        [Route("AddAppartement")]

        public async Task<ActionResult> AddAppartement(SellingAppartementDto NewAppartement)
        {
/*            if (NewAppartement.UserId != null)
            {
                return BadRequest("UserId isn't Required");
            }

            var _User = await UserManager.GetUserAsync(User);

            NewAppartement.UserId = _User.Id;*/

            _UsersManger.AddAppartement(NewAppartement);

            return Ok();

                
        }
    }
}
