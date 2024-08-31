using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;
using PartyUp.Services.Interfaces;

namespace PartyUp.Services.Implementations
{
    public class BusinessEntityService : IBusinessEntityService
    {
        private readonly IBusinessEntityRepository _beRepository;

        public BusinessEntityService(IBusinessEntityRepository businessEntityRepository)
        {
            _beRepository = businessEntityRepository;
        }
        public async Task AddBusinessEntity(BusinessEntityRequestDTO businessEntity)
        {
            await _beRepository.AddBusinessEntity(businessEntity);
        }
        public async Task DeleteBusinessEntity(int businessEntityId)
        {
            await _beRepository.DeleteBusinessEntity(businessEntityId);
        }
        public async Task<IEnumerable<BusinessEntityResponseDTO>> GetAllBusinessEntities()
        {
            return await _beRepository.GetAllBusinessEntities();
        }
        public async Task<BusinessEntityResponseDTO> GetBusinessEntityById(int businessEntityId)
        {
            return await _beRepository.GetBusinessEntityById(businessEntityId);
        }
        public async Task<IEnumerable<BusinessEntityResponseDTO>> GetBusinessEntitiesByName(string name)
        {
            return await _beRepository.GetBusinessEntitiesByName(name);
        }
        public async Task UpdateBusinessEntity(BusinessEntityRequestDTO requestBody, int businessEntityId)
        {
            await _beRepository.UpdateBusinessEntity(requestBody, businessEntityId);
        }
    }
}
