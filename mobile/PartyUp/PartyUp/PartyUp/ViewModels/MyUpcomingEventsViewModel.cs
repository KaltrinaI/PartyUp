using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PartyUp.Models;
using PartyUp.Services;
using PartyUp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PartyUp.ViewModels
{
    public class MyUpcomingEventsViewModel : BaseViewModel
    {
        private readonly IEventService _eventService;

        private ObservableCollection<Event> _myUpcomingEvents;
        public ObservableCollection<Event> MyUpcomingEvents { get => _myUpcomingEvents;
            set => SetProperty(ref _myUpcomingEvents, value);
        }
        
        public ICommand RefreshEventsCommand { get; }
        private bool _isRefreshing;
        public bool IsRefreshing{
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        public Command<Event> NavigateToEventDetailsCommand { get; }
        public MyUpcomingEventsViewModel(IEventService eventService)
        {
            _eventService = eventService;
            LoadEvents();
            RefreshEventsCommand = new Command(LoadEvents);
            NavigateToEventDetailsCommand = new Command<Event>(async (e) => await NavigateToEventDetails(e));
        }

        private async Task NavigateToEventDetails(Event partyEvent)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EventDetailsPage(partyEvent, false));
        }
        
        private void LoadEvents()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsRefreshing = true;

                var userId = Preferences.Get("userId","");
                var events = await _eventService.GetAllUpcomingEventsAsync(userId).ConfigureAwait(false);
                MyUpcomingEvents = new ObservableCollection<Event>(events.OrderBy(x => x.DateTimeOfEvent));
                IsRefreshing = false;
            });
        }

    }
}