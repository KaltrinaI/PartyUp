using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Services.Interfaces
{
    public interface IBusinessEntityService
    {
        Task<BusinessEntityResponseDTO> GetBusinessEntityById(int businessEntityId);
        Task<IEnumerable<BusinessEntityResponseDTO>> GetBusinessEntitiesByName(string name);
        Task<IEnumerable<BusinessEntityResponseDTO>> GetAllBusinessEntities();
        Task AddBusinessEntity(BusinessEntityRequestDTO businessEntity);
        Task UpdateBusinessEntity(BusinessEntityRequestDTO requestBody, int businessEntityId);
        Task DeleteBusinessEntity(int businessEntityId);
    }
}
