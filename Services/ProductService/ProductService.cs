using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresApi.Services.ProductService
{
    public class ProductService : BaseApiService, IProductService
    {
        public ProductService(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var product = _mapper.Map<Product>(newProduct);
                var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == product.CategoryId);
                if (category == null)
                {
                    throw new BadHttpRequestException($"Cannot find categoryId: {product.CategoryId}", 400);
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                var lists = await GetProducts();
                serviceResponse.Data = lists.Data;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var product =
                    await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)
                {
                    throw new Exception($"Category cannot find id : {id}");
                }
                _db.Products.Remove(product);

                await _db.SaveChangesAsync();
                var lists = await GetProducts();
                serviceResponse.Data = lists.Data;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProducts()
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var dbProducts = await _db.Products.ToListAsync();
            var categories = await _db.Categories.ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => new GetProductDto
            {
                Id = c.Id,
                Name = c.Name,
                ModelYear = c.ModelYear,
                ListPrice = c.ListPrice,
                CategoryId = c.CategoryId,
                CategoryName = categories.FirstOrDefault(x => x.Id == c.CategoryId).Name,
            }).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetProductDto>> GetProductById(long id)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                var dbProduct = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
                var categories = await _db.Categories.ToListAsync();
                if (dbProduct == null)
                {
                    throw new Exception($"Product cannot find id: {id}");
                }
                serviceResponse.Data = new GetProductDto
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    ModelYear = dbProduct.ModelYear,
                    ListPrice = dbProduct.ListPrice,
                    CategoryId = dbProduct.CategoryId,
                    CategoryName = categories.FirstOrDefault(x => x.Id == dbProduct.CategoryId).Name,
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct
            (UpdateProductDto update)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                var product =
                   await _db.Products.FirstOrDefaultAsync(c => c.Id == update.Id);
                if (product == null)
                {
                    throw new Exception($"Product cannot find id : {update.Id}");
                }

                product.Name = update.Name;
                product.ListPrice = update.ListPrice;
                product.ModelYear = update.ModelYear;
                product.CategoryId = update.CategoryId;

                await _db.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetProductDto>(product);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
