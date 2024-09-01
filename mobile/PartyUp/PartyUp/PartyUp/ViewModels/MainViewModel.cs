using Autofac;
using PartyUp.Services;

namespace PartyUp.ViewModels
{
    public class MainViewModel
    {
        public ProfileViewModel ProfileViewModel { get; }
        public MyUpcomingEventsViewModel MyUpcomingEventsViewModel { get; }
        public ExploreEventsViewModel ExploreEventsViewModel { get; }
        
        public MainViewModel(ProfileViewModel profileViewModel, MyUpcomingEventsViewModel myUpcomingEventsViewModel, ExploreEventsViewModel exploreEventsViewModel)
        {
            ProfileViewModel = profileViewModel;
            MyUpcomingEventsViewModel = myUpcomingEventsViewModel;
            ExploreEventsViewModel = exploreEventsViewModel;
        }
    }
}