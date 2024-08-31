using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Services.Interfaces;

namespace PartyUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddEvent(EventRequestDTO @event)
        {
            await _eventService.AddEvent(@event);
            return Ok();
        }

        [HttpDelete("{eventId}")]
        [Authorize]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {
            await _eventService.DeleteEvent(eventId);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetAllEvents()
        {
            var response = await _eventService.GetAllEvents();
            return Ok(response);
        }

        [HttpGet("{eventId}")]
        [Authorize]
        public async Task<ActionResult<EventResponseDTO>> GetEventById(int eventId)
        {
            var response = await _eventService.GetEventById(eventId);
            return Ok(response);
        }

        [HttpGet("eventName/{name}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetEventsByName(string name)
        {
            var response = await _eventService.GetEventsByName(name);
            return Ok(response);
        }

        [HttpGet("eventLocation/{address}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetEventsByLocation(string address)
        {
            var response = await _eventService.GetEventsByLocation(address);
            return Ok(response);
        }

        [HttpGet("upcomingEvents/{date}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetUpcomingEvents(DateTime date)
        {
            var response = await _eventService.GetUpcomingEvents(date);
            return Ok(response);
        }

        [HttpPut("{eventId}")]
        [Authorize]
        public async Task<ActionResult> UpdateEvent(EventRequestDTO requestBody, int eventId)
        {
            await _eventService.UpdateEvent(requestBody, eventId);
            return Ok();
        }

        [HttpGet("attended/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetAttendedEvents(string userId)
        {
            var response = await _eventService.GetPastEvents(userId);
            return Ok(response);
        }

        [HttpGet("upcoming/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetUpcomingEvents(string userId)
        {
            var response = await _eventService.GetUpcomingEvents(userId);
            
            return Ok(response);
        }

        [HttpPost("explore")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> ExploreEvents([FromBody] EventFilter eventFilter)
        {
            var response = await _eventService.ExploreEvents(eventFilter);
            return Ok(response);
        }
    }
}
