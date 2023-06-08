using BL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyApartmentController : ControllerBase
    {
        
        BuyApartmentManger _buyApartment;
        public BuyApartmentController(BuyApartmentManger buyApartment)
        { 
            _buyApartment= buyApartment;
        }
        [HttpGet]
        public ActionResult<List<ApartmentList>> GetAll()
        { 
            return _buyApartment.GetAll();
        }

        [HttpGet]
        public ActionResult<ApartmentList> Get(int id)
        {
            return _buyApartment.Get(id);
        }
    }
}
