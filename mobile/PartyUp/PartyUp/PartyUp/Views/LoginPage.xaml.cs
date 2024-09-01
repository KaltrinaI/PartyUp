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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            try
            {
                BindingContext = App.Container.Resolve<LoginViewModel>();

            }
            catch (Exception e)
            {
                
            }
        }
    }
}