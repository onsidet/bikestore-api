using BikeStoresApi.Dtos;
using BikeStoresApi.Models;

namespace BikeStoresApi.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetProducts();
        Task<ServiceResponse<GetProductDto>> GetProductById(long id);
        Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newCategory);
        Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateCategory);
        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(long id);
    }
}
