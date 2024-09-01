using System.Collections.Generic;
using System.Threading.Tasks;
using PartyUp.Models;

namespace PartyUp.Services
{
    public class BusinessEntityService : IBusinessEntityService
    {
        private readonly IHttpService _httpService;

        public BusinessEntityService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<BusinessEntity>> GetAllBusinessEntitiesAsync()
        {
            return await _httpService.GetAsync<IEnumerable<BusinessEntity>>("/api/BusinessEntity");
        }

        public async Task<BusinessEntity> GetBusinessEntityByIdAsync(string businessEntityId)
        {
            return await _httpService.GetAsync<BusinessEntity>($"api/BusinessEntity/businessEntity/{businessEntityId}");
        }

        public async Task<BusinessEntity> CreateBusinessEntityAsync(BusinessEntity newBusinessEntity)
        {
            return await _httpService.PostAsync<BusinessEntity>("api/BusinessEntity", newBusinessEntity);
        }

        public async Task<BusinessEntity> UpdateBusinessEntityAsync(string businessEntityId, BusinessEntity updatedBusinessEntity)
        {
            return await _httpService.PutAsync<BusinessEntity>($"api/BusinessEntity/{businessEntityId}", updatedBusinessEntity);
        }

        public async Task<bool> DeleteBusinessEntityAsync(string businessEntityId)
        {
            return await _httpService.DeleteAsync($"api/BusinessEntity/{businessEntityId}");
        }
    }
}