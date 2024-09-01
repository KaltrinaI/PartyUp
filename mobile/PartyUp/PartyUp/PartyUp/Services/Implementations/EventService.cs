using System.Collections.Generic;
using System.Threading.Tasks;
using PartyUp.Models;
using PartyUp.ViewModels;

namespace PartyUp.Services
{
    public class EventService : IEventService
    {
        private readonly IHttpService _httpService;

        public EventService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        // Event Methods
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _httpService.GetAsync<IEnumerable<Event>>("api/event");
        }

        public async Task<IEnumerable<Event>> GetAllEventsAttendedAsync(string userId)
        {
            return await _httpService.GetAsync<IEnumerable<Event>>($"api/event/attended/{userId}");
        }
        

        public async Task<Event> GetEventByIdAsync(string eventId)
        {
            return await _httpService.GetAsync<Event>($"api/event/{eventId}");
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            return await _httpService.PostAsync<Event>("api/event", newEvent);
        }

        public async Task<Event> UpdateEventAsync(string eventId, Event updatedEvent)
        {
            return await _httpService.PutAsync<Event>($"api/event/{eventId}", updatedEvent);
        }

        public async Task<bool> DeleteEventAsync(string eventId)
        {
            return await _httpService.DeleteAsync($"api/event/{eventId}");
        }

        public async Task<IEnumerable<Event>> GetExploreEvents(EventFilter eventFilter)
        {
            return await _httpService.PostAsync<IEnumerable<Event>>("api/event/explore", eventFilter);
        }

        public Task RequestReservation(ReservationRequestDTO request)
        {
             return _httpService.PostAsync("api/reservationrequest", request);
        }

        public async Task<IEnumerable<Event>> GetAllUpcomingEventsAsync(string userId)
        {
            return await _httpService.GetAsync<IEnumerable<Event>>($"api/event/upcoming/{userId}");
        }
    }
}