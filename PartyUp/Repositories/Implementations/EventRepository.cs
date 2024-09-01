using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyUp.Data;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PartyUp.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EventRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddEvent(EventRequestDTO @event)
        {
            var existingOrganizer = await _context.BusinessEntities.FirstOrDefaultAsync(be => be.BusinessId == @event.Organizer.BusinessId);

            if (existingOrganizer == null)
            {
                throw new Exception("Organizer not found");
            }
            Event requestBody = new Event();
            requestBody.EventName = @event.Name;
            requestBody.DateTimeOfEvent = @event.DateTimeOfEvent;
            requestBody.Description = @event.Description;
            requestBody.Location = _mapper.Map<Location>(@event.Location);
            requestBody.Organizer = existingOrganizer;
            requestBody.PosterUrl = @event.PosterUrl;
            requestBody.EventTax = @event.EventTax;
            requestBody.NumberOfReservations = @event.NumberOfReservations;
            requestBody.Tags = @event.Tags;

            _context.Events.Add(requestBody);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(int eventId)
        {
            var @event = await _context.Events.FindAsync(eventId);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEvents()
        {
            var events = await _context.Events
                .Include(e => e.Location)
                .Include(e=>e.Tags)
                .ToListAsync();

            var eventDTOs = events.Select(e => new EventResponseDTO
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Description = e.Description,
                DateTimeOfEvent = e.DateTimeOfEvent,
                Location = e.Location != null ? _mapper.Map<LocationDTO>(e.Location) : null,
                PosterUrl = e.PosterUrl,
                EventTax = e.EventTax,
                NumberOfReservations = e.NumberOfReservations,
                Tags = e.Tags?.ToList(),
            }).ToList();

            return eventDTOs;
        }

        public async Task<EventResponseDTO> GetEventById(int eventId)
        {
            var @event = await _context.Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (@event == null)
            {
                return null;
            }

            var eventDTO = new EventResponseDTO
            {
                EventId = @event.EventId,
                EventName = @event.EventName,
                Description = @event.Description,
                DateTimeOfEvent = @event.DateTimeOfEvent,
                Location = @event.Location != null ? _mapper.Map<LocationDTO>(@event.Location) : null,
                PosterUrl = @event.PosterUrl,
                EventTax = @event.EventTax,
                NumberOfReservations = @event.NumberOfReservations,
                Tags = @event.Tags.ToList()
            };

            return eventDTO;
        }

        public async Task<IEnumerable<EventResponseDTO>> GetEventsByName(string name)
        {
            var events = await _context.Events
                .Include(e => e.Location) 
                .Where(e => e.EventName == name)
                .ToListAsync();

            var eventDTOs = events.Select(e => new EventResponseDTO
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Description = e.Description,
                DateTimeOfEvent = e.DateTimeOfEvent,
                Location = e.Location != null ? _mapper.Map<LocationDTO>(e.Location) : null,
                PosterUrl = e.PosterUrl,
                EventTax = e.EventTax,
                NumberOfReservations = e.NumberOfReservations,
                Tags = e.Tags.ToList() 
            }).ToList();

            return eventDTOs;
        }

        public async Task<IEnumerable<EventResponseDTO>> GetUpcomingEvents(DateTime date)
        {
            var events = await _context.Events
                .Include(e => e.Location) 
                .Where(e => e.DateTimeOfEvent >= date)
                .ToListAsync();

            var eventDTOs = events.Select(e => new EventResponseDTO
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Description = e.Description,
                DateTimeOfEvent = e.DateTimeOfEvent,
                Location = e.Location != null ? _mapper.Map<LocationDTO>(e.Location) : null,
                PosterUrl = e.PosterUrl,
                EventTax = e.EventTax,
                NumberOfReservations = e.NumberOfReservations,
                Tags = e.Tags.ToList()
            }).ToList();

            return eventDTOs;
        }

        public async Task<IEnumerable<EventResponseDTO>> GetEventsByLocation(string address)
        {
            var events = await _context.Events
                .Include(e => e.Location) 
                .Where(e => e.Location.Address.ToLower().Trim() == address.ToLower().Trim())
                .ToListAsync();

            if (events == null || !events.Any())
            {
                
                Console.WriteLine("No events found for the provided address.");
                return Enumerable.Empty<EventResponseDTO>();
            }

            var eventDTOs = events.Select(e => new EventResponseDTO
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Description = e.Description,
                DateTimeOfEvent = e.DateTimeOfEvent,
                Location = e.Location != null ? _mapper.Map<LocationDTO>(e.Location) : null,
                PosterUrl = e.PosterUrl,
                EventTax = e.EventTax,
                NumberOfReservations = e.NumberOfReservations,
                Tags = e.Tags.ToList()
            }).ToList();

            return eventDTOs;
        }

        public async Task UpdateEvent(EventRequestDTO requestBody, int eventId)
        {
           
            var @event = await _context.Events.FindAsync(eventId);

            if (@event == null)
            {
                throw new Exception("Event not found");
            }

            var existingOrganizer = @event.Organizer != null
                ? await _context.BusinessEntities.FirstOrDefaultAsync(be => be.BusinessId == @event.Organizer.BusinessId)
                : null;

            @event.EventName = requestBody.Name;
            @event.DateTimeOfEvent = requestBody.DateTimeOfEvent;
            @event.Description = requestBody.Description;
            @event.Location = _mapper.Map<Location>(requestBody.Location);

            if (existingOrganizer != null)
            {
                @event.Organizer = existingOrganizer;
            }

            @event.PosterUrl = requestBody.PosterUrl;
            @event.EventTax = requestBody.EventTax;
            @event.NumberOfReservations = requestBody.NumberOfReservations;
            @event.Tags = requestBody.Tags;

            _context.Entry(@event).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<EventResponseDTO>> GetPastEvents(string userId)
        {
            var pastEvents = await _context.ReservationRequests
                .Include(rr => rr.Event)
                                .Include(rr => rr.Event.Location)
                .Where(rr => rr.UserId == userId && rr.Status == ReservationStatus.Confirmed && rr.Event.DateTimeOfEvent < DateTime.UtcNow)
                .Select(rr => rr.Event)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EventResponseDTO>>(pastEvents);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetUpcomingEvents(string userId)
        {
            var upcomingEvents = await _context.ReservationRequests
                .Include(rr => rr.Event)
                .Include(rr => rr.Event.Location)
                .Where(rr => rr.UserId == userId && rr.Status == ReservationStatus.Requested && rr.Event.DateTimeOfEvent >= DateTime.UtcNow)
                .Select(rr => rr.Event)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EventResponseDTO>>(upcomingEvents);
        }

    }
}
