using System.Windows.Input;
using PartyUp.Models;
using PartyUp.Services;
using PartyUp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PartyUp.ViewModels
{
    public class ReservationRequestDTO
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
        public ReservationStatus Status { get; set; }
        public int NrOfPeople { get; set; }
    }
    
    public class EventDetailsViewModel
    {
        public Event PartyEvent { get; }
        public ICommand ReserveRequestCommand { get; }
        
        public bool IsExploring { get; }

        public ICommand ViewCommentsCommand { get; }

        public EventDetailsViewModel(Event partyEvent, bool isExploring, IEventService eventService)
        {
            PartyEvent = partyEvent;
            IsExploring = isExploring;
            ReserveRequestCommand = new Command(async () =>
            {
                var req = new ReservationRequestDTO
                {
                    UserId = Preferences.Get("userId",""),
                    EventId = partyEvent.EventId,
                    NrOfPeople = 1,
                    Status = ReservationStatus.Requested
                };
                await eventService.RequestReservation(req);
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            
            ViewCommentsCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CommentsPage(partyEvent));
            });
        }
    }
}