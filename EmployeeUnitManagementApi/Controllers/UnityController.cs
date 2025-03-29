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
    public class UnityController : ControllerBase
    {
        private readonly IUnityService _unityService;

        public UnityController(IUnityService unityService)
        {
            _unityService = unityService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUnity([FromBody] Unity entity)
        {
            if (entity == null)
                return BadRequest("Os dados da unidade são inválidos.");

            GenericResponse response = await _unityService.Create(entity);

            if (response.IsSuccess == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateUnity([FromBody] UnityUpdateRequest request)
        {
            if (request == null || request.Id <= 0)
                return BadRequest("Os dados da unidade são inválidos.");

            GenericResponse response = await _unityService.Update(request);

            if (response.IsSuccess == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(IEnumerable<Unity>), 200)]
        public async Task<IActionResult> GetAllUnity()
        {
            return Ok(await _unityService.SelectAll(new Unity()));
        }

        [HttpGet("getAssociated")]
        [ProducesResponseType(typeof(IEnumerable<CollaboratorAssociatedResponse>), 200)]
        public async Task<IActionResult> GetAllUnityCollaboratorAssociated()
        {
            return Ok(await _unityService.SelectCollaboratorAssociated(new Unity()));
        }
    }
}
