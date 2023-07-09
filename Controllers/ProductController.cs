using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using BikeStoresApi.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresApi.Controllers
{
    public class ProductController : BaseApiController<IProductService>
    {
        public ProductController(IServiceProvider provider) : base(provider)
        {
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> Get()
        {
            return Ok(await _service.GetProducts());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> GetById(long id)
        {
            var response = await _service.GetProductById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> AddCategory(AddProductDto newCategory)
        {
            return Ok(await _service.AddProduct(newCategory));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> UpdateCategory(UpdateProductDto updateCategory)
        {
            var response = await _service.UpdateProduct(updateCategory);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> DeleteCategory(long id)
        {
            var response = await _service.DeleteProduct(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
