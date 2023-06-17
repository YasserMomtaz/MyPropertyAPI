using BL.Dtos;
using BL.Mangers;
using Microsoft.AspNetCore.Http;
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
        [Route("/search")]
        public async Task<ActionResult<List<ApartmentList>>> Search(string City, string Address, int minArea, int maxArea , int minPrice, int maxPrice)
        {
            var list = await _buyApartment.Search( City, Address, minArea, maxArea,  minPrice, maxPrice);
            return list.ToList();


        }


    }
}
