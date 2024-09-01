using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PartyUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartyUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            try
            {
                var mainViewModel = App.Container.Resolve<MainViewModel>();
                BindingContext = mainViewModel;
                ProfilePage.BindingContext = mainViewModel.ProfileViewModel;
                MyUpcomingEventsPage.BindingContext = mainViewModel.MyUpcomingEventsViewModel;
                ExploreEventsPage.BindingContext = mainViewModel.ExploreEventsViewModel;

            }
            catch (Exception e)
            {
                
            }
            

        }
    }
}