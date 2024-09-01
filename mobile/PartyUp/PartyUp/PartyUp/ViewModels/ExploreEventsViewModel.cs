using System;
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
    public class ExploreEventsViewModel : BaseViewModel
    {
        private readonly IEventService _eventService;
        public EventFilter EventFilter { get; set; }

        public ObservableCollection<Tag> AvailableTags { get; set; }

        private ObservableCollection<Event> _allEvents;
        public ObservableCollection<Event> AllEvents
        {
            get => _allEvents; set=> SetProperty(ref _allEvents, value); 
        }
        public ICommand RemoveTagCommand { get; }

        public ExploreEventsViewModel(IEventService eventService)
        {
            _eventService = eventService;
            EventFilter = new EventFilter
            {
                Tags = new ObservableCollection<Tag>(),
                LocationAddress = "",
                Text = "",
                UserId = Preferences.Get("userId","")
            };

            AvailableTags = new ObservableCollection<Tag>();
            AllEvents = new ObservableCollection<Event>();
            RemoveTagCommand = new Command<Tag>(OnRemoveTag);
            NavigateToEventDetailsCommand = new Command<Event>(async (e) => await NavigateToEventDetails(e));
            RefetchEventsCommand = new Command(async () =>
            {
                AllEvents = new ObservableCollection<Event>(await LoadEvents());
            });
            Device.BeginInvokeOnMainThread(async () =>
            {
                var ec = await LoadEvents().ConfigureAwait(false);
                AllEvents = new ObservableCollection<Event>(ec);
            });
            LoadTags();
            
        }

        public ICommand RefetchEventsCommand { get; set; }

        private void OnRemoveTag(Tag tag)
        {
            if (EventFilter.Tags.Contains(tag))
            {
                EventFilter.Tags.Remove(tag);
                AvailableTags.First(tg => tg.Id == tag.Id).IsChecked = false;
            }
        }
        

    private Task<IEnumerable<Event>> LoadEvents()
    {
        return _eventService.GetExploreEvents(EventFilter);
    }

    public Command<Event> NavigateToEventDetailsCommand { get; }

        private async Task NavigateToEventDetails(Event partyEvent)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EventDetailsPage(partyEvent,true));
        }


        private void LoadTags()
        {
            var tagObjects = Enum.GetValues(typeof(TagEnum))
                .Cast<TagEnum>()
                .Select(tg => new Tag
                {
                    Name = tg.ToString(),
                    Color = GetBackgroundColor(tg),
                    Id = (int) tg+1,
                    IsChecked = false
                });

            AvailableTags = new ObservableCollection<Tag>(tagObjects);
        }
        
        private static readonly Dictionary<TagEnum, string> tagColors = new Dictionary<TagEnum, string>
        {
            { TagEnum.Pop, "#8B0000" },          // Dark Red
            { TagEnum.RnB, "#4B0082" },          // Indigo
            { TagEnum.Rock, "#2F4F4F" },         // Dark Slate Gray
            { TagEnum.Jazz, "#000080" },         // Navy
            { TagEnum.Classical, "#2E8B57" },    // Sea Green
            { TagEnum.HipHop, "#556B2F" },       // Dark Olive Green
            { TagEnum.Blues, "#191970" },        // Midnight Blue
            { TagEnum.Folk, "#8B4513" },         // Saddle Brown
            { TagEnum.Latin, "#8B0000" },        // Dark Red
            { TagEnum.Indie, "#483D8B" },        // Dark Slate Blue
            { TagEnum.Alternative, "#2F4F4F" },  // Dark Slate Gray
            { TagEnum.Techno, "#008080" },       // Teal
            { TagEnum.Disco, "#800000" }         // Maroon
        };

        public static string GetBackgroundColor(TagEnum tag)
        {
            return tagColors.TryGetValue(tag, out var color) ? color : "#000000"; // Default to Black if not found
        }
    }
    
    public enum TagEnum
    {
        Pop,
        RnB,
        Rock,
        Jazz,
        Classical,
        HipHop,
        Blues,
        Folk,
        Latin,
        Indie,
        Alternative,
        Techno,
        Disco
    }
}