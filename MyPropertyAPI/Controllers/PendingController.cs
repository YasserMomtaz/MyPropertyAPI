using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPropertyAPI.Controllers
{
    [Route("api/[controller]")]
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
          return _pendingPropertyManager.GetAll();
          
        }
        [HttpGet]
        [Route("/details/{id}")]
        public ActionResult<PendingReadDto> GetById(int id)
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
        [HttpGet]
        [Route("{id}")]
        public ActionResult Accept(int id)
        {
            _pendingPropertyManager.Accept(id);
            return NoContent();
        }

    }
}
