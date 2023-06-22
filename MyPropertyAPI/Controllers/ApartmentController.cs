using BL.Dtos;
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
		[Route("/buy")]
		public async Task<ActionResult<List<ApartmentList>>> GetAllBuy()
		{
			var list = await _buyApartment.GetAll("Buy");
			return list.ToList();
		}

		[HttpGet]
		[Route("/rent")]
		public async Task<ActionResult<List<ApartmentList>>> GetAllRent()
		{
			var list = await _buyApartment.GetAll("Rent");
			return list.ToList();
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
		[Route("/search")]
		public async Task<ActionResult<List<ApartmentList>>> Search(string City, string Address, int minArea, int maxArea, int minPrice, int maxPrice, string type)
		{
			var list = await _buyApartment.Search(City, Address, minArea, maxArea, minPrice, maxPrice, type);
			return list.ToList();

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
	}
}
