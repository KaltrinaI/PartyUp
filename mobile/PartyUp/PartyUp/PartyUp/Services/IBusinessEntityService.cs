using System.Collections.Generic;
using System.Threading.Tasks;
using PartyUp.Models;

namespace PartyUp.Services
{
    public interface IBusinessEntityService
    {
        Task<IEnumerable<BusinessEntity>> GetAllBusinessEntitiesAsync();
        Task<BusinessEntity> GetBusinessEntityByIdAsync(string businessId);
        Task<BusinessEntity> CreateBusinessEntityAsync(BusinessEntity newBusinessEntity);
        Task<BusinessEntity> UpdateBusinessEntityAsync(string businessId, BusinessEntity updatedBusinessEntity);
        Task<bool> DeleteBusinessEntityAsync(string businessId);
    }
}