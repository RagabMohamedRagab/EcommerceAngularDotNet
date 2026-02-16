using AutoMapper;
using Ecom.API.Helpers;
using Ecom.core.Dtos;
using Ecom.core.Entities.Models;
using Ecom.core.Interfaces;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpGet("get-all-product")]
        public async Task<ActionResult<ResponsePageResult<ProductDto>>> GetAll()
        {
            try
            {
                var result = _unitOfWork.Product.GetAll(b => b.Category, b => b.ProductPhotos);
                var MappResult=_mapper.Map<List<ProductDto>>(result);
                if (MappResult == null || !MappResult.Any())
                {
                    return NotFound(new ResponsePageResult<ProductDto>() { Message = "No products found.", IsSucess = false, Entities = [], Status = 404 });
                }
                return Ok(new ResponsePageResult<ProductDto>() { Message = "Done", IsSucess = true, Entities = MappResult, Status = 200 });
            }
            catch
            {
                return BadRequest(new ResponsePageResult<ProductDto>() { Message = "No products found.", IsSucess = false, Entities = [], Status = 404 });
            }
        }
        [HttpPost("add-Product")]
        public async Task<ActionResult<ResponseResult<string>>> AddProduct(AddProudct productDto)
        {
            try
            {
                 var mappedProduct = _mapper.Map<Product>(productDto);
               await _unitOfWork.Product.AddAsync(mappedProduct);
                await _unitOfWork.CommitAsync();
                return Ok(new ResponseResult<string>(true));
            }
            catch
            {
                return BadRequest(new ResponseResult<string>() { Message= "An error occurred while adding the product." });
            }

        }
    }
}
