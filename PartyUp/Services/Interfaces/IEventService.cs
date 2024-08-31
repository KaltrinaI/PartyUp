using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventResponseDTO> GetEventById(int eventId);
        Task<IEnumerable<EventResponseDTO>> GetEventsByName(string name);
        Task<IEnumerable<EventResponseDTO>> GetUpcomingEvents(DateTime date);
        Task<IEnumerable<EventResponseDTO>> GetEventsByLocation(string address);
        Task<IEnumerable<EventResponseDTO>> GetPastEvents(string userId);
        Task<IEnumerable<EventResponseDTO>> GetUpcomingEvents(string userId);
        Task<IEnumerable<EventResponseDTO>> GetAllEvents();
        Task AddEvent(EventRequestDTO @event);
        Task UpdateEvent(EventRequestDTO requestBody, int eventId);
        Task DeleteEvent(int eventId);
        Task<IEnumerable<EventResponseDTO>> ExploreEvents(EventFilter eventFilter);
    }
}
