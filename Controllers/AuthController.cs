using BikeStoresApi.Dtos;
using BikeStoresApi.Models;
using BikeStoresApi.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<long>>> Register(UserRigisterDto request)
        {
            var response = await _authService.Register
                (
                new User { Username = request.Username }, request.Password
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto login)
        {
            var response = await _authService.Login(login.Username, login.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
