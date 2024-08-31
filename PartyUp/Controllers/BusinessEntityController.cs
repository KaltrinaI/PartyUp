using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Services.Interfaces;

namespace PartyUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessEntityController : ControllerBase
    {
        private readonly IBusinessEntityService _beService;

        public BusinessEntityController(IBusinessEntityService businessEntityService)
        {
            _beService = businessEntityService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddBusinessEntity(BusinessEntityRequestDTO businessEntity)
        {
            await _beService.AddBusinessEntity(businessEntity);
            return Ok();
        }

        [HttpDelete("{businessEntityId}")]
        [Authorize]
        public async Task<ActionResult> DeleteBusinessEntity(int businessEntityId)
        {
            await _beService.DeleteBusinessEntity(businessEntityId);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BusinessEntityResponseDTO>>> GetAllBusinessEntities()
        {
            var response = await _beService.GetAllBusinessEntities();
            return Ok(response);
        }

        [HttpGet("businessEntity/{businessEntityId}")]
        [Authorize]
        public async Task<ActionResult<BusinessEntityResponseDTO>> GetBusinessEntityById(int businessEntityId)
        {
            var response = await _beService.GetBusinessEntityById(businessEntityId);
            return Ok(response);
        }

        [HttpGet("businessEntities/{name}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BusinessEntityResponseDTO>>> GetBusinessEntityByName(string name)
        {
            var response = await _beService.GetBusinessEntitiesByName(name);
            return Ok(response);
        }

        [HttpPut("{businessEntityId}")]
        [Authorize]
        public async Task<ActionResult> UpdateBusinessEntity(BusinessEntityRequestDTO requestBody, int businessEntityId)
        {
            await _beService.UpdateBusinessEntity(requestBody, businessEntityId);
            return Ok();
        }

    }
}
