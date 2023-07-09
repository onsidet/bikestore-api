using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using BikeStoresApi.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresApi.Controllers
{
    public class ProductController : BaseController<IProductService>
    {
        public ProductController(IServiceProvider provider) : base(provider)
        {
        }

        [HttpGet()]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> ListsAsync()
        {
            return Ok(await _service.ListsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> FindAsync(long id)
        {
            var response = await _service.FindAsync(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> AddAsync(AddProductDto newCategory)
        {
            return Ok(await _service.AddAsync(newCategory));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> UpdateAsync(UpdateProductDto updateCategory)
        {
            var response = await _service.UpdateAsync(updateCategory);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> DeleteAsync(long id)
        {
            var response = await _service.DeleteAsync(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
