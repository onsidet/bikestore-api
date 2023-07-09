using AutoMapper;
using BikeStoresApi.Data;

namespace BikeStoresApi.Services.BaseService
{
    public class BaseService
    {
        protected readonly IServiceProvider _provider;
        protected readonly DataContext _db;
        protected readonly IMapper _mapper;

        public BaseService(IServiceProvider provider)
        {
            _provider = provider;
            _db = provider.GetService<DataContext>();
            _mapper = provider.GetService<IMapper>();
        }
    }
}
