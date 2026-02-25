using Ecom.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var thing = await _unitOfWork.Category.GetById(999);
            if (thing == null) return NotFound();
            return Ok(thing);
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> ServerError()
        {
            var thing = await _unitOfWork.Category.GetById(999);
            thing.Name = "";
            return Ok(thing);
        }
        [HttpGet("bad-request/{id}")]
        public async Task<ActionResult> BadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> BadRequest()
        {
            return BadRequest("");
        }


    }
}
