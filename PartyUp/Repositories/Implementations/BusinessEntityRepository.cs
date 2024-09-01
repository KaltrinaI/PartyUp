using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyUp.Data;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;

namespace PartyUp.Repositories.Implementations
{
    public class BusinessEntityRepository : IBusinessEntityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BusinessEntityRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddBusinessEntity(BusinessEntityRequestDTO businessEntity)
        {
            if (businessEntity == null)
            {
                throw new ArgumentNullException(nameof(businessEntity), "Business entity data cannot be null.");
            }

            var existingOwner = await _context.Users.FirstOrDefaultAsync(u => u.Email == businessEntity.OwnerEmail);

            if (existingOwner == null)
            {
                throw new KeyNotFoundException($"Owner with email {businessEntity.OwnerEmail} not found.");
            }

            BusinessEntity requestBody = _mapper.Map<BusinessEntity>(businessEntity);
            requestBody.Owner = existingOwner;

            _context.BusinessEntities.Add(requestBody);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusinessEntity(int businessEntityId)
        {
            var businessEntity = await _context.BusinessEntities.FindAsync(businessEntityId);
            if (businessEntity != null)
            {
                _context.BusinessEntities.Remove(businessEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BusinessEntityResponseDTO>> GetAllBusinessEntities()
        {
            var entities = await _context.BusinessEntities
                .Include(be => be.Owner)
                .Include(be => be.Location)
                .Include(be => be.Events)
                .ToListAsync();

            var entityDTOs = entities.Select(entity => new BusinessEntityResponseDTO
            {
                BusinessEntityId = entity.BusinessEntityId,
                OwnerEmail = entity.Owner.Email,
                Name = entity.Name,
                BusinessId = entity.BusinessId,
                Location = new LocationDTO
                {
                    Address = entity.Location.Address,
                    Latitude = entity.Location.Latitude,
                    Longitude = entity.Location.Longitude
                },
                Events = entity.Events.Select(ev => new EventResponseDTO
                {
                    EventId = ev.EventId,
                    EventName = ev.EventName,
                    Description = ev.Description,
                    DateTimeOfEvent = ev.DateTimeOfEvent,
                    Location = ev.Location != null ? new LocationDTO
                    {
                        Address = ev.Location.Address,
                        Latitude = ev.Location.Latitude,
                        Longitude = ev.Location.Longitude
                    } : null,
                    PosterUrl = ev.PosterUrl,
                    EventTax = ev.EventTax,
                    NumberOfReservations = ev.NumberOfReservations,
                    Tags = ev.Tags.ToList()
                }).ToList()
            });

            return entityDTOs;
        }

        public async Task<BusinessEntityResponseDTO> GetBusinessEntityById(int businessEntityId)
        {
            var entity = await _context.BusinessEntities
                .Include(be => be.Owner)
                .Include(be => be.Location)
                .Include(be => be.Events)
                .FirstOrDefaultAsync(be => be.BusinessEntityId == businessEntityId);

            if (entity == null)
            {
                return null;
            }

            var entityDTO = new BusinessEntityResponseDTO
            {
                BusinessEntityId = entity.BusinessEntityId,
                OwnerEmail = entity.Owner.Email,
                Name = entity.Name,
                BusinessId = entity.BusinessId,
                Location = new LocationDTO
                {
                    Address = entity.Location.Address,
                    Latitude = entity.Location.Latitude,
                    Longitude = entity.Location.Longitude
                },
                Events = entity.Events.Select(ev => new EventResponseDTO
                {
                    EventId = ev.EventId,
                    EventName = ev.EventName,
                    Description = ev.Description,
                    DateTimeOfEvent = ev.DateTimeOfEvent,
                    Location = ev.Location != null ? new LocationDTO
                    {
                        Address = ev.Location.Address,
                        Latitude = ev.Location.Latitude,
                        Longitude = ev.Location.Longitude
                    } : null,
                    PosterUrl = ev.PosterUrl,
                    EventTax = ev.EventTax,
                    NumberOfReservations = ev.NumberOfReservations,
                    Tags = ev.Tags.ToList()
                }).ToList()
            };

            return entityDTO;
        }

        public async Task<IEnumerable<BusinessEntityResponseDTO>> GetBusinessEntitiesByName(string name)
        {
            var entities = await _context.BusinessEntities
                .Include(be => be.Owner)
                .Include(be => be.Location)
                .Include(be => be.Events)
                .Where(be => be.Name == name)
                .ToListAsync();

            if (!entities.Any())
            {
                return Enumerable.Empty<BusinessEntityResponseDTO>();
            }

            var entityDTOs = entities.Select(entity => new BusinessEntityResponseDTO
            {
                BusinessEntityId = entity.BusinessEntityId,
                OwnerEmail = entity.Owner.Email,
                Name = entity.Name,
                BusinessId = entity.BusinessId,
                Location = new LocationDTO
                {
                    Address = entity.Location.Address,
                    Latitude = entity.Location.Latitude,
                    Longitude = entity.Location.Longitude
                },
                Events = entity.Events.Select(ev => new EventResponseDTO
                {
                    EventId = ev.EventId,
                    EventName = ev.EventName,
                    Description = ev.Description,
                    DateTimeOfEvent = ev.DateTimeOfEvent,
                    Location = ev.Location != null ? new LocationDTO
                    {
                        Address = ev.Location.Address,
                        Latitude = ev.Location.Latitude,
                        Longitude = ev.Location.Longitude
                    } : null,
                    PosterUrl = ev.PosterUrl,
                    EventTax = ev.EventTax,
                    NumberOfReservations = ev.NumberOfReservations,
                    Tags = ev.Tags.ToList()
                }).ToList()
            });

            return entityDTOs;
        }

        public async Task UpdateBusinessEntity(BusinessEntityRequestDTO requestBody, int businessEntityId)
        {
            var businessEntity = await _context.BusinessEntities.FindAsync(businessEntityId);
            var existingOwner = await _context.Users.FirstOrDefaultAsync(u => u.Email == requestBody.OwnerEmail);

            if (businessEntity != null)
            {
                if (existingOwner != null)
                {
                    businessEntity.Owner = existingOwner;
                }
                businessEntity.Name = requestBody.Name;
                businessEntity.Location = _mapper.Map<Location>(requestBody.Location);
                businessEntity.Events = _mapper.Map<IEnumerable<Event>>(requestBody.Events).ToList();
                businessEntity.BusinessId = requestBody.BusinessId;

                _context.Entry(businessEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
