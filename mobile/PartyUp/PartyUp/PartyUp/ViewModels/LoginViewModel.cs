using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PartyUp.Services;
using PartyUp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PartyUp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        
        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
                LoginCommand = new Command(async()=>await OnLogin());
                SignUpCommand = new Command(async()=> await OnSignUp());

        }

        private async Task OnLogin()
        {
            var user = await _userService.LoginAsync(Email, Password);

            await SecureStorage.SetAsync("jwt_token", user.Token);
            Preferences.Set("userId", user.UserId);
            Preferences.Set("user", JsonConvert.SerializeObject(user));

            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        private Task OnSignUp()
        {
            return Application.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }
    }
}