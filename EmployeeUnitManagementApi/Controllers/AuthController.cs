using EmployeeUnitManagementDomain.Models.Constants;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EmployeeUnitManagementApi.Controllers
{
    [Route("ManagementApi/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("token")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            if (request.Username == ConstantSystem.UserAuth && request.Password == ConstantSystem.PasswordAuth)
                return Ok(_jwtService.GenerateToken(request.Username));

            return Unauthorized("Credenciais inválidas.");
        }

        [HttpGet("check")]
        [Authorize]
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "Autorizado." });
        }
    }
}
