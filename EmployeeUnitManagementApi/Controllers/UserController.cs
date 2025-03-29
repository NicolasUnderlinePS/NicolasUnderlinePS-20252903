using EmployeeUnitManagementDomain.Interfaces.Service;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeUnitManagementApi.Controllers
{
    [Route("ManagementApi/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] User entity)
        {
            if (entity == null)
                return BadRequest("Os dados do usuário são inválidos.");

            GenericResponse response = await _userService.Create(entity);

            if (response.IsSuccess == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            if (request == null || request.Id <= 0)
                return BadRequest("Os dados do usuário são inválidos.");

            GenericResponse response = await _userService.Update(request);

            if (response.IsSuccess == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> SelectAllUser([FromQuery] bool? statusActive)
        {
            return Ok(await _userService.SelectAll(new User(statusActive)));
        }
    }
}
