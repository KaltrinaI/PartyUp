using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Repositories.Interfaces
{
    public interface IEventRepository
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
    }
}
