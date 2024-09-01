using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PartyUp.Models;
using PartyUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartyUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailsPage : ContentPage
    {
        public EventDetailsPage(Event partyEvent, bool isExploring)
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<EventDetailsViewModel>( new []
            {
                new PositionalParameter(0,partyEvent),
                new PositionalParameter(1,isExploring)
            });
        }
    }
}