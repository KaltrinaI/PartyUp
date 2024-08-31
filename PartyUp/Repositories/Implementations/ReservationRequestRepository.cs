using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyUp.Data;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;

namespace PartyUp.Repositories.Implementations
{
    public class ReservationRequestRepository : IReservationRequestRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ReservationRequestRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddReservationRequest(ReservationRequestDTO? reservationRequestDTO)
        {
            if (reservationRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(reservationRequestDTO), "Reservation request data cannot be null.");
            }

            var existingEvent = await _context.Events.FindAsync(reservationRequestDTO.EventId);

            if (existingEvent == null)
            {
                throw new Exception("Event not found");
            }

            var reservationRequest = new ReservationRequest
            {
                UserId = reservationRequestDTO.UserId,
                EventId = reservationRequestDTO.EventId,
                Status = reservationRequestDTO.Status,
                NrOfPeople = reservationRequestDTO.NrOfPeople
            };

            await _context.ReservationRequests.AddAsync(reservationRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationRequest(int reservationRequestId)
        {
            var reservationRequest = await _context.ReservationRequests.FindAsync(reservationRequestId);
            if (reservationRequest != null)
            {
                _context.ReservationRequests.Remove(reservationRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetAllReservationRequests()
        {
            var reservationRequests = await _context.ReservationRequests
                .Include(rr => rr.Event) 
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservationRequests);
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetAllReservationRequestsForUser(string userId)
        {
            var datetimeNow = DateTime.UtcNow;
            var reqs = (await _context.ReservationRequests.Include(rr => rr.Event).ToListAsync()).Where(x => x.UserId == userId && x.Event.DateTimeOfEvent - datetimeNow > TimeSpan.Zero);

            return _mapper.Map<IEnumerable<ReservationResponseDTO>>(reqs);
        }

        public async Task<ReservationResponseDTO> GetReservationRequestById(int reservationRequestId)
        {
            
            var reservationRequest = await _context.ReservationRequests
                .Include(rr => rr.Event) 
                .FirstOrDefaultAsync(rr => rr.ReservationRequestId == reservationRequestId);

            
            if (reservationRequest == null)
            {
                return null;
            }

            return _mapper.Map<ReservationResponseDTO>(reservationRequest);
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetReservationRequestsByStatus(ReservationStatus status)
        {
            var reservationRequests = await _context.ReservationRequests
                .Include(rr => rr.Event) 
                .Where(rr => rr.Status == status)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservationRequests);
        }

        public async Task UpdateReservationRequest(ReservationRequestDTO requestBody, int reservationRequestId)
        {
            var reservationRequest = await _context.ReservationRequests.FindAsync(reservationRequestId);

            if (reservationRequest != null)
            {
                reservationRequest.EventId = requestBody.EventId;
                reservationRequest.NrOfPeople = requestBody.NrOfPeople;
                reservationRequest.Status = requestBody.Status;

                _context.Entry(reservationRequest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

    }
}
