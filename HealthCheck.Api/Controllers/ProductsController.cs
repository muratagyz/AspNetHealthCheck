using HealthCheck.Api.Dtos;
using HealthCheck.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthCheck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll();

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto model)
        {
            var response = await _productService.Add(model);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.Delete(id);

            return CreateActionResultInstance(response);
        }


        IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
