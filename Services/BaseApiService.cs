using AutoMapper;
using BikeStoresApi.Data;

namespace BikeStoresApi.Services
{
    public class BaseApiService
    {
        protected readonly IServiceProvider _provider;
        protected readonly DataContext _db;
        protected readonly IMapper _mapper;

        public BaseApiService(IServiceProvider provider)
        {
            _provider = provider;
            _db = provider.GetService<DataContext>();
            _mapper = provider.GetService<IMapper>();
        }
    }
}
