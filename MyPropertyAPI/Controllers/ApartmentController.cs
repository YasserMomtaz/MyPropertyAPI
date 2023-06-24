using BL.Dtos;

using BL.Dtos.UserDtos;

using BL.Dtos.Apartment;

using BL.Mangers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DAL.Data.Models;

namespace MyPropertyAPI.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ApartmentController : ControllerBase
	{


        private readonly IapartmentManger _buyApartment;
        private readonly UserManager<IdentityUser> UserManagerFromPackage;
        public ApartmentController(IapartmentManger buyApartment, UserManager<IdentityUser> usermanger)
		{
			_buyApartment = buyApartment;
			UserManagerFromPackage = usermanger;
		}

		[HttpGet]
		[Route("/buy/{page}/{pageCount}")]
		public async Task<ActionResult<ApartmentListPaginationDto>> GetAllBuy(int page ,int pageCount)
		{
			string userId = "0";
			var user = await UserManagerFromPackage.GetUserAsync(User);
			if(user != null)
			{
                userId=user.Id;
            }
			var list = await _buyApartment.GetAll("Buy",page,pageCount,userId);
			return list;
		}

		[HttpGet]
		[Route("/rent/{page}/{pageCount}")]
        public async Task<ActionResult<ApartmentListPaginationDto>> GetAllrent(int page, int pageCount)
        {
            string userId = "0";
            var user = await UserManagerFromPackage.GetUserAsync(User);
            if (user != null)
            {
                userId = user.Id;
            }

            var list = await _buyApartment.GetAll("Rent", page, pageCount, userId);
			return list;
		}

		[HttpGet]
		[Route("/{id}")]
		public async Task<ActionResult<ApartmentDetails>> Get(int id)
		{
            string userId = "0";
            var user = await UserManagerFromPackage.GetUserAsync(User);
            if (user != null)
            {
                userId = user.Id;
            }

            return _buyApartment.GetApartmentDetails(id,userId);
		}
/*        [Authorize(Policy = "User")]
*/        [HttpGet]
		[Route("/allfavorites")]
		public async Task<ActionResult<List<ApartmentList>>> GetAddedToFavorite()
		{
			//var user = await UserManagerFromPackage.GetUserAsync(User);
			string _id = "1";

			//_id= user.Id;
            var FavApart = _buyApartment.GetAddedToFavorite(_id);
			return FavApart.ToList();


		}
/*        [Authorize(Policy = "User")]*/
        [HttpPost]
		[Route("/addtofavorite/{apartId}")]
		public async Task<ActionResult<ApartmentDetails>> AddToFavorite(int apartId)
		{
			string userId = "1";
			//var user = await UserManagerFromPackage.GetUserAsync(User);
			//userId= user.Id;
            _buyApartment.AddToFavorite(userId, apartId);
			return Ok();
		}
        [HttpDelete]
        [Route("/removeFromFavorite/{apartId}")]
        public async Task<ActionResult<ApartmentDetails>> RemoveFromFavorite(int apartId)
        {
            string userId = "1";
			//var user = await UserManagerFromPackage.GetUserAsync(User);
			//userId= user.Id;
			_buyApartment.RemoveFromFavorite(userId, apartId);
            return Ok();
        }
        /*        [Authorize(Policy = "User")]*/
        [HttpGet]
		[Route("/getuserapartment/")]
		public async  Task<ActionResult<List<ApartmentList>>> GetAllUserApartments()
		{
            //var user = await UserManagerFromPackage.GetUserAsync(User);
            string id = "2";
            //id= user.Id;
            var UserApart = _buyApartment.GetAllUserApartments(id);
			if (UserApart == null)
			{
				return BadRequest();
			}
			return UserApart.ToList();


		}

    
    [HttpGet]
        [Route("/search/{page}/{CountPerPage}")]
        public async Task<ActionResult<ApartmentListPaginationDto>> Search(int page, int CountPerPage, string City, string Address, int minArea, int maxArea , int minPrice, int maxPrice, string type)
        {
            string userId = "0";
            var user = await UserManagerFromPackage.GetUserAsync(User);
            if (user != null)
            {
                userId = user.Id;
            }

            var list = await _buyApartment.Search(page,CountPerPage ,City, Address, minArea, maxArea,  minPrice, maxPrice, type,userId);
			return list;

        }


/*        [Authorize(Policy = "Broker")]*/
        [HttpGet]
        [Route("/getBrokerApartment")]
        public async Task<ActionResult<List<ApartmentList>>> GetBrokerApartment()
        {
            string brokerId = "1";
			//var user = await UserManagerFromPackage.GetUserAsync(User);
			//brokerId = user.Id;
			var list = await _buyApartment.GetAppartmentsOfBroker(brokerId);

            return list.ToList();

        }
/*        [Authorize(Policy = "Admin")]
        [Authorize(Policy = "Broker")]*/
        [HttpPost]
        [Route("/sellAppartement")]
        public  ActionResult SoldAppartement(SoldAppartementDto soldAppartement)
		{
			var state= _buyApartment.SellAppartement(soldAppartement);
			if (state == 1)
			{
				return NoContent();
			}
			else
			{
				return BadRequest("Appartement Not Exist");
			}

		}
/*        [Authorize(Policy = "Admin")]
		[Authorize(Policy = "Broker")]*/
        [HttpDelete]
        [Route("/DeleteAppartement/{Id}")]
        public ActionResult DeleteAppartement(int Id)
		{
			var State = _buyApartment.DeleteAppartement(Id);
			if (State == 1)
			{
				return NoContent();
			}
			else
			{
				return BadRequest("Appartement Not Exist");
			}
        }




    }

}
