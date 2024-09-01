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
    public partial class ExploreEventsPage : ContentPage
    {
        public ExploreEventsPage()
        {
            InitializeComponent();
        }
    }
}