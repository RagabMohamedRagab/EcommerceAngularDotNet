using Ecom.core.Interfaces;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        [Route("get-all")]
        [HttpPost]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result=_unitOfWork.Category.GetAll();
                return Ok(result);
            }
            catch
            {
                return BadRequest("An error occurred while retrieving categories.");
            }
        }
    }
}
