using BikeStoresApi.Models;

namespace BikeStoresApi.Services.BaseService
{
    public interface IBaseApiService<TResponse, TAdd, TUpdate>
    {
        Task<ServiceResponse<List<TResponse>>> ListsAsync();
        Task<ServiceResponse<TResponse>> FindAsync(long id);
        Task<ServiceResponse<List<TResponse>>> AddAsync(TAdd model);
        Task<ServiceResponse<TResponse>> UpdateAsync(TUpdate model);
        Task<ServiceResponse<List<TResponse>>> DeleteAsync(long id);
    }
}
