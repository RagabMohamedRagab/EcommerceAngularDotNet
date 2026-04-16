using Ecom.API.Helpers;
using Ecom.core.Entities;
using Ecom.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class BasketController : BaseController
    {
        public BasketController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet("get-basket-item/{id}")]
        public async Task<ActionResult<ResponseResult<CustomerBasket>>> GetBasketById(string id)
        {
            try
            {
                var basket = await _unitOfWork.CustomerBasketItem.GetCustomerBasket(id);
                if (basket == null)
                {
                    var newBasket = new CustomerBasket(id);
                    var createdBasket = await _unitOfWork.CustomerBasketItem.UpdateCustomerBasket(newBasket);
                    return Ok(new ResponseResult<CustomerBasket>() { Message = "New basket created", IsSucess = true, Entity = createdBasket ?? newBasket, Status = 200 });
                }
                return Ok(new ResponseResult<CustomerBasket>() { Message = "Done", IsSucess = true, Entity = basket, Status = 200 });
            }
            catch
            {
                return BadRequest(new ResponseResult<CustomerBasket>() { Message = "An error occurred while retrieving the basket.", IsSucess = false, Entity = null, Status = 400 });
            }
        }

        [HttpPost("update-basket")]
        public async Task<ActionResult<ResponseResult<CustomerBasket>>> UpdateBasket([FromBody] CustomerBasket customerBasket)
        {
            try
            {
                var updateBasket = await _unitOfWork.CustomerBasketItem.UpdateCustomerBasket(customerBasket);
                if (updateBasket == null)
                {
                    return BadRequest(new ResponseResult<CustomerBasket>() { Message = "Failed to update basket", IsSucess = false, Entity = null, Status = 400 });
                }
                return Ok(new ResponseResult<CustomerBasket>() { Message = "Done", IsSucess = true, Entity = updateBasket, Status = 200 });
            }
            catch
            {
                return BadRequest(new ResponseResult<CustomerBasket>() { Message = "An error occurred while updating the basket.", IsSucess = false, Entity = null, Status = 400 });
            }
        }

        [HttpDelete("delete-basket-item/{id}")]
        public async Task<ActionResult<ResponseResult<string>>> DeleteBasket(string id)
        {
            try
            {
                var result = await _unitOfWork.CustomerBasketItem.DeleteCustomerBasket(id);
                if (result)
                {
                    return Ok(new ResponseResult<string>() { Message = "Deleted successfully", IsSucess = true, Entity = "Done", Status = 200 });
                }
                return BadRequest(new ResponseResult<string>() { Message = "Failed to delete basket", IsSucess = false, Entity = null, Status = 400 });
            }
            catch
            {
                return BadRequest(new ResponseResult<string>() { Message = "An error occurred while deleting the basket.", IsSucess = false, Entity = null, Status = 400 });
            }
        }
    }
}
