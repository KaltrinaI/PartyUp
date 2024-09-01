using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PartyUp.Services;
using PartyUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartyUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<SignUpViewModel>();
        }
    }
}