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
    public partial class CommentsPage : ContentPage
    {
        public CommentsPage(Event partyEvent)
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<CommentsViewModel>( new []
            {
                new PositionalParameter(0,partyEvent)
            });
        }
    }
}