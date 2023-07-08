using AutoMapper;
using BikeStoresApi.Data;
using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var product = _mapper.Map<Product>(newProduct);
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == product.CategoryId);
                if (category == null)
                {
                    throw new BadHttpRequestException($"Cannot find categoryId: {product.CategoryId}", 400);
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
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
                    await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)
                {
                    throw new Exception($"Category cannot find id : {id}");
                }
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();
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
            var dbProducts = await _context.Products.ToListAsync();
            var categories = await _context.Categories.ToListAsync();
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
                var dbProduct = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
                var categories = await _context.Categories.ToListAsync();
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
                   await _context.Products.FirstOrDefaultAsync(c => c.Id == update.Id);
                if (product == null)
                {
                    throw new Exception($"Product cannot find id : {update.Id}");
                }

                product.Name = update.Name;
                product.ListPrice = update.ListPrice;
                product.ModelYear = update.ModelYear;
                product.CategoryId = update.CategoryId;

                await _context.SaveChangesAsync();

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
