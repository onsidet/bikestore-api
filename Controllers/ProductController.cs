using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using BikeStoresApi.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresApi.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> Get()
        {
            return Ok(await _productService.GetProducts());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> GetById(long id)
        {
            var response = await _productService.GetProductById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> AddCategory(AddProductDto newCategory)
        {
            return Ok(await _productService.AddProduct(newCategory));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> UpdateCategory(UpdateProductDto updateCategory)
        {
            var response = await _productService.UpdateProduct(updateCategory);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> DeleteCategory(long id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
