using System.Collections.Generic;
using System.Threading.Tasks;
using PartyUp.Models;
using PartyUp.ViewModels;

namespace PartyUp.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetAllEventsAttendedAsync(string userId);
        Task<IEnumerable<Event>> GetAllUpcomingEventsAsync(string userId);

        Task<Event> GetEventByIdAsync(string eventId);
        Task<Event> CreateEventAsync(Event newEvent);
        
        Task<Event> UpdateEventAsync(string eventId, Event updatedEvent);
        Task<bool> DeleteEventAsync(string eventId);
        Task<IEnumerable<Event>> GetExploreEvents(EventFilter eventFilter);
        Task RequestReservation(ReservationRequestDTO request);
    }
}