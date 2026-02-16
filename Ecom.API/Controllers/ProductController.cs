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
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository,IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
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
        public async Task<ActionResult<ResponseResult<string>>> AddProduct([FromForm]AddProudct productDto)
        {
            try
            {
                var result =await _productRepository.AddProduct(productDto);
                if (result)
                {
                    return Ok(new ResponseResult<string>(true));
                }
                throw new Exception();
            }
            catch
            {
                return BadRequest(new ResponseResult<string>() { Message = "An error occurred while adding the product." });
            }

        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<ResponseResult<ProductDto>>> GetById(int Id)
        {
            try
            {
                var result = await _unitOfWork.Product.GetById(Id, b => b.Category, b => b.ProductPhotos);
                if (result == null)
                {
                    return NotFound(new ResponseResult<ProductDto>() { Message = "Product not found.", IsSucess = false, Entity = null, Status = 404 });
                }
                var MappResult = _mapper.Map<ProductDto>(result);
                return Ok(new ResponseResult<ProductDto>() { Message = "Done", IsSucess = true, Entity = MappResult, Status = 200 });
            }
            catch
            {
                return BadRequest(new ResponseResult<ProductDto>() { Message = "An error occurred while retrieving the product.", IsSucess = false, Entity = null, Status = 400 });

            }
        }
    }
}
