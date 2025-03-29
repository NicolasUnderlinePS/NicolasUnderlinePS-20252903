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
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCollaborator([FromBody] Collaborator entity)
        {
            if (entity == null)
                return BadRequest("Os dados do usuário são inválidos.");

            GenericResponse response = await _collaboratorService.Create(entity);

            if (response.IsSuccess == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateCollaborator([FromBody] CollaboratorUpdateRequest request)
        {
            if (request == null || request.Id <= 0)
                return BadRequest("Os dados do usuário são inválidos.");

            GenericResponse response = await _collaboratorService.Update(request);

            if (response.IsSuccess == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(IEnumerable<Collaborator>), 200)]
        public async Task<IActionResult> SelectAllCollaborator()
        {
            return Ok(await _collaboratorService.SelectAll(new Collaborator()));
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteCollaborator(int id)
        {
            if (id <= 0)
                return BadRequest("Colaborador inválido.");

            GenericResponse response = await _collaboratorService.Delete(new Collaborator() { Id = id });

            if (!response.IsSuccess)
                return BadRequest(response.Message);

            return Ok(response);
        }

    }
}
