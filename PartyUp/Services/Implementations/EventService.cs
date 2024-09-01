using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Implementations;
using PartyUp.Repositories.Interfaces;
using PartyUp.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace PartyUp.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IReservationRequestRepository _reservationRequestRepository;

        public EventService(IEventRepository eventRepository, IReservationRequestRepository reservationRequestRepository)
        {
            _eventRepository = eventRepository;
            _reservationRequestRepository = reservationRequestRepository;

        }
        public async Task AddEvent(EventRequestDTO @event)
        {
            await _eventRepository.AddEvent(@event);
        }

        public async Task DeleteEvent(int eventId)
        {
            await _eventRepository.DeleteEvent(eventId);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEvents()
        {
            return await _eventRepository.GetAllEvents();
        }

        public async Task<EventResponseDTO> GetEventById(int eventId)
        {
            return await _eventRepository.GetEventById(eventId);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetEventsByName(string name)
        {
            return await _eventRepository.GetEventsByName(name);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetEventsByLocation(string address)
        {
            return await _eventRepository.GetEventsByLocation(address);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetUpcomingEvents(DateTime date)
        {
            return await _eventRepository.GetUpcomingEvents(date);
        }

        public async Task UpdateEvent(EventRequestDTO requestBody, int eventId)
        {
            await _eventRepository.UpdateEvent(requestBody, eventId);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetPastEvents(string userId)
        {
            return await _eventRepository.GetPastEvents(userId);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetUpcomingEvents(string userId)
        {
            return await _eventRepository.GetUpcomingEvents(userId);
        }

        public async Task<IEnumerable<EventResponseDTO>> ExploreEvents(EventFilter eventFilter)
        {
            var dateTimeNow = DateTime.UtcNow;

            var events = (await _eventRepository.GetAllEvents()).Where(x => (x.DateTimeOfEvent - dateTimeNow) > TimeSpan.Zero)
                .Where(e => string.IsNullOrEmpty(eventFilter.Text) || e.EventName.Contains(eventFilter.Text, StringComparison.OrdinalIgnoreCase))
                .Where(v => string.IsNullOrEmpty(eventFilter.LocationAddress) || v.Location.Address.Contains(eventFilter.LocationAddress, StringComparison.OrdinalIgnoreCase))
                .Where(t => eventFilter.Tags == null || eventFilter.Tags.Count()==0 || t.Tags.Any(b => eventFilter.Tags.Any(v=>v.Id==b.Id) ));

            var reser = (await _reservationRequestRepository.GetAllReservationRequestsForUser(eventFilter.UserId))
                .Where(x => x?.Event?.EventName != null)
                .Select(x => x.Event.EventName);

            return events.Where(e => !reser.Contains(e.EventName)); 
        }


    }
}
