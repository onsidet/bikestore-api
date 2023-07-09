using AutoMapper;
using BikeStoresApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TService> : ControllerBase where TService : class
    {
        protected readonly IServiceProvider _provider;
        protected readonly DataContext _db;
        protected readonly TService _service;
        protected readonly IMapper _mapper;

        public BaseController(IServiceProvider provider)
        {
            _provider = provider;
            _db = provider.GetService<DataContext>();
            _service = provider.GetService<TService>();
            _mapper = provider.GetService<IMapper>();
        }
    }
}
