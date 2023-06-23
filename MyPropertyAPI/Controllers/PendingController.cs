using BL.Dtos.PendingProperty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPropertyAPI.Controllers
{
/*    [Authorize(Policy = "Admin")]
*/    [Route("api/[controller]")]
    [ApiController]
    public class PendingController : ControllerBase
    {
        private readonly IPendingPropertyManager _pendingPropertyManager;
        public PendingController( IPendingPropertyManager pendingPropertyManager )
        {
            _pendingPropertyManager = pendingPropertyManager;
        }
        [HttpGet]
        public ActionResult<List<PendingReadDto>> GetAll() { 
            var pendingReq = _pendingPropertyManager.GetAll();
            if ( pendingReq == null ) { return BadRequest(); }
            return pendingReq.ToList();
          
        }
        [HttpGet]
        [Route("/details/{id}")]
        public ActionResult<PendingReadDetailsDto> GetById(int id)
        {
            var appartment = _pendingPropertyManager.GetById(id);
            if (appartment == null) { return NotFound(); }
            return Ok(appartment);


        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var a = _pendingPropertyManager.GetById(id);
            if(a== null) { return NotFound(); }
            _pendingPropertyManager.Delete(id);
           

            return NoContent();
        }
        [HttpPatch]
        [Route("{id}")]
        public ActionResult Accept(int id, string brokerId)
        {
            string managerId = "3";
          var apartment= _pendingPropertyManager.Accept(id, brokerId, managerId);
            if (apartment == null) { return BadRequest(); }

            return NoContent();
        }
        [HttpGet]
        [Route("/brokerId")]
        public ActionResult<List<BrokerDataDto>> GetAllBroker()
        {
            return _pendingPropertyManager.GetAllBroker();

        }
        
    }

}
