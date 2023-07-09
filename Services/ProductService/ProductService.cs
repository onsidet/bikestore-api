using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using BikeStoresApi.Services.BaseService;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresApi.Services.ProductService
{
    public class ProductService : BaseApiService, IProductService
    {
        public ProductService(IServiceProvider provider) : base(provider)
        {
        }
        public async Task<ServiceResponse<List<GetProductDto>>> ListsAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var entries = await _db.Products.ToListAsync();
            var categories = await _db.Categories.ToListAsync();
            serviceResponse.Data = entries.Select(x => new GetProductDto
            {
                Id = x.Id,
                Name = x.Name,
                ModelYear = x.ModelYear,
                ListPrice = x.ListPrice,
                CategoryId = x.CategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == x.CategoryId).Name,
            }).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> FindAsync(long id)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                var entry = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
                var categories = await _db.Categories.ToListAsync();
                if (entry == null)
                {
                    throw new Exception($"Product cannot find id: {id}");
                }
                serviceResponse.Data = new GetProductDto
                {
                    Id = entry.Id,
                    Name = entry.Name,
                    ModelYear = entry.ModelYear,
                    ListPrice = entry.ListPrice,
                    CategoryId = entry.CategoryId,
                    CategoryName = categories.FirstOrDefault(x => x.Id == entry.CategoryId).Name,
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> AddAsync(AddProductDto model)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var entry = _mapper.Map<Product>(model);
                var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == entry.CategoryId);
                if (category == null)
                {
                    throw new BadHttpRequestException($"Cannot find categoryId: {entry.CategoryId}", 400);
                }
                _db.Products.Add(entry);
                await _db.SaveChangesAsync();
                var lists = await ListsAsync();
                serviceResponse.Data = lists.Data;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateAsync(UpdateProductDto model)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                var entry =
                   await _db.Products.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (entry == null)
                {
                    throw new Exception($"Product cannot find id : {model.Id}");
                }

                entry.Name = model.Name;
                entry.ListPrice = model.ListPrice;
                entry.ModelYear = model.ModelYear;
                entry.CategoryId = model.CategoryId;

                await _db.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetProductDto>(entry);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteAsync(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var entry =
                    await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (entry == null)
                {
                    throw new Exception($"Category cannot find id : {id}");
                }
                _db.Products.Remove(entry);

                await _db.SaveChangesAsync();
                var lists = await ListsAsync();
                serviceResponse.Data = lists.Data;
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
