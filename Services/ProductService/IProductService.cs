using BikeStoresApi.Dtos;
using BikeStoresApi.Services.BaseService;

namespace BikeStoresApi.Services.ProductService
{
    public interface IProductService : IBaseApiService<GetProductDto, AddProductDto, UpdateProductDto>
    {
    }
}
