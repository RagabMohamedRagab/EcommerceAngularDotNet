using AutoMapper;
using Ecom.API.Helpers;
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
        public async Task<ActionResult<ResponsePageResult<Category>>> GetAll()
        {
            try
            {
                var result = _unitOfWork.Category.GetAll();
                return Ok(new ResponsePageResult<Category>
                {
                    IsSucess = true,
                    Message = "Categories retrieved successfully.",
                    Status = 200,
                    Entities = result.ToList(),
                    TotalCount = result.Count()
                });
            }
            catch
            {
                return BadRequest(new ResponsePageResult<Category>());
            }
        }

        [HttpGet("Get-by-id/{id}")]
        public async Task<ActionResult<ResponseResult<Category>>> GetById(int id)
        {
            try
            {
                var result =await _unitOfWork.Category.GetById(id);
                if (result == null)
                {
                    return Ok(new ResponseResult<Category>(false,Message:"object Not Found"));
                }
                return Ok(new ResponseResult<Category>() { Entity = result, Status = 200, Message = "Done", IsSucess = true });
            }
            catch
            {
                return BadRequest(new ResponseResult<Category>(false));
            }
        }

        [Route("add-Category")]
        [HttpPost]
        public async Task<ActionResult<ResponseResult<CategoryDto>>> AddCategory(CategoryDto category)
        {
            try
            {
                var map=_mapper.Map<Category>(category);
                var result = _unitOfWork.Category.AddAsync(map);
                await _unitOfWork.CommitAsync();
                return Ok(new ResponseResult<CategoryDto>() { Entity = category });
            }
            catch
            {
                return BadRequest(new ResponseResult<CategoryDto>(false));
            }
        }
        [HttpDelete("Delete-Category/{id}")]
        public async Task<ActionResult<ResponseResult<string>>> DeleteCategory(int id)
        {
            try
            {
                var Category = await _unitOfWork.Category.GetById(id);
                if (Category == null)
                {
                    return NotFound(new ResponseResult<string>
                    {
                        IsSucess = false,
                        Message = "Object not found",
                        Status = 404,
                        Entity = null
                    });
                }

                await _unitOfWork.Category.DeleteAsync(Category);
                await _unitOfWork.CommitAsync();

                return Ok(new ResponseResult<string>
                {
                    IsSucess = true,
                    Message = "Deleted successfully",
                    Status = 200,
                    Entity = "Done"
                });
            }
            catch
            {
                return BadRequest(new ResponseResult<string>
                {
                    IsSucess = false,
                    Message = "An error occurred while deleting the category.",
                    Status = 400,
                    Entity = null
                });
            }
        }
        [HttpPut("Update-Category")]
        public async Task<ActionResult<ResponseResult<string>>> UpdateCategory(UpdateCategory category)
        {
            try
            {
                var mapp=_mapper.Map<Category>(category);
                await _unitOfWork.Category.UpdateAsync(mapp);
                await _unitOfWork.CommitAsync();
                return Ok(new ResponseResult<string>() { IsSucess=true,Message="Done"});
            }
            catch
            {
                return BadRequest(new ResponseResult<string>() { Message="error "});
            }
        }
    }
}
