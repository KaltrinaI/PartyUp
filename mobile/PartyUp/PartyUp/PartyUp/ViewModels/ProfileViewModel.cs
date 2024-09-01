using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PartyUp.Models;
using PartyUp.Services;
using PartyUp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PartyUp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private ObservableCollection<Event> _myAttendedEvents;
        public ObservableCollection<Event> MyAttendedEvents
        {
            get => _myAttendedEvents;
            set => SetProperty(ref _myAttendedEvents, value);
        }
        
        public ICommand NavigateToEventDetailsCommand { get; }

        private User _user;
        private readonly IEventService _eventService;
        public User User { get=>_user; set => SetProperty(ref _user,value); }

        public ProfileViewModel(IUserService userService, IEventService eventService)
        {
            
            _userService = userService;
            _eventService = eventService;
            LoadUserPastEventsAsync();
            NavigateToEventDetailsCommand = new Command<Event>(async (e) => await NavigateToEventDetails(e));
            Device.BeginInvokeOnMainThread(async () =>
            {
                await LoadUserProfileAsync().ConfigureAwait(false);
                User = JsonConvert.DeserializeObject<User>(Preferences.Get("user", null));

            });
        }
        private async Task NavigateToEventDetails(Event partyEvent)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EventDetailsPage(partyEvent, false));
        }

        private async Task LoadUserPastEventsAsync()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
            var userId = Xamarin.Essentials.Preferences.Get("userId", "");
            var events = await _eventService.GetAllEventsAttendedAsync(userId);
            MyAttendedEvents = new ObservableCollection<Event>(events.OrderBy(x => x.DateTimeOfEvent));
            });
        }        
        private Task LoadUserProfileAsync()
        {
            var userId = Xamarin.Essentials.Preferences.Get("userId", "");
            return _userService.GetUserByIdAsync(userId);
        }
    
    }
    
}