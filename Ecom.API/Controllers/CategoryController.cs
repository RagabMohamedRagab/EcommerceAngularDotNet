using AutoMapper;
using Ecom.core.Dtos;
using Ecom.core.Entities.Models;
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
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper) : base(unitOfWork)
        {
            _mapper= mapper;
        }


        [Route("get-all")]
        [HttpPost]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = _unitOfWork.Category.GetAll();
                return Ok(result);
            }
            catch
            {
                return BadRequest("An error occurred while retrieving categories.");
            }
        }

        [HttpGet("Get-by-id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var result = _unitOfWork.Category.GetById(id);
                if (result == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("An error occurred while retrieving the category.");
            }
        }

        [Route("add-Category")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryDto category)
        {
            try
            {
                var map=_mapper.Map<Category>(category);
                var result = _unitOfWork.Category.AddAsync(map);
                await _unitOfWork.CommitAsync();
                return Ok("Done");
            }
            catch
            {
                return BadRequest("An error occurred while adding the category.");
            }
        }
        [HttpDelete("Delete-Category/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var Category = await _unitOfWork.Category.GetById(id);
                if (Category == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                } 
               await _unitOfWork.Category.DeleteAsync(Category);
                await _unitOfWork.CommitAsync();
                return Ok("Done");
            }
            catch 
            {
                return BadRequest("An error occurred while deleting the category.");
              
            }
        }
        [HttpPut("Update-Category")]
        public async Task<ActionResult> UpdateCategory(UpdateCategory category)
        {
            try
            {
                var mapp=_mapper.Map<Category>(category);
                await _unitOfWork.Category.UpdateAsync(mapp);
                await _unitOfWork.CommitAsync();
                return Ok("Done");
            }
            catch
            {
                return BadRequest("An error occurred while Updating the category.");
            }
        }
    }
}
