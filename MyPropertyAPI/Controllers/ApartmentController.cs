using BL.Dtos;

using BL.Dtos.UserDtos;

using BL.Dtos.Apartment;

using BL.Mangers;
using Microsoft.AspNetCore.Mvc;

namespace MyPropertyAPI.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ApartmentController : ControllerBase
	{


		IapartmentManger _buyApartment;
		public ApartmentController(IapartmentManger buyApartment)
		{
			_buyApartment = buyApartment;
		}

		[HttpGet]
		[Route("/buy/{page}/{pageCount}")]
		public async Task<ActionResult<ApartmentListPaginationDto>> GetAllBuy(int page ,int pageCount)
		{
			var list = await _buyApartment.GetAll("Buy",page,pageCount);
			return list;
		}

		[HttpGet]
		[Route("/rent/{page}/{pageCount}")]
        public async Task<ActionResult<ApartmentListPaginationDto>> GetAllrent(int page, int pageCount)
        {
			var list = await _buyApartment.GetAll("Rent", page, pageCount);
			return list;
		}

		[HttpGet]
		[Route("/{id}")]
		public ActionResult<ApartmentDetails> Get(int id)
		{
			return _buyApartment.GetApartmentDetails(id);
		}
		[HttpGet]
		[Route("/allfavorites")]
		public ActionResult<List<ApartmentList>> GetAddedToFavorite()
		{
			string id = "1";
			var FavApart = _buyApartment.GetAddedToFavorite(id);
			return FavApart.ToList();


		}
		[HttpPost]
		[Route("/addtofavorite/{apartId}")]
		public ActionResult<ApartmentDetails> AddToFavorite(int apartId)
		{
			string userId = "1";
			_buyApartment.AddToFavorite(userId, apartId);
			return Ok();
		}

		[HttpGet]
		[Route("/getuserapartment/")]
		public ActionResult<List<ApartmentList>> GetAllUserApartments()
		{
			string id = "2";
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
            var list = await _buyApartment.Search(page,CountPerPage ,City, Address, minArea, maxArea,  minPrice, maxPrice, type);
			return list;

        }



        [HttpGet]
        [Route("/getBrokerApartment")]
        public async Task<ActionResult<List<ApartmentList>>> GetBrokerApartment()
        {
			var list = await _buyApartment.GetAppartmentsOfBroker();

            return list.ToList();

        }

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
