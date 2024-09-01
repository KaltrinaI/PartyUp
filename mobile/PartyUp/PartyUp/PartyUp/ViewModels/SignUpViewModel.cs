using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PartyUp.Models;
using PartyUp.Services;
using PartyUp.Views;
using Xamarin.Forms;

namespace PartyUp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _email;
        private string _password;
        private string _confirmPassword;
        
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => SetProperty(ref _dateOfBirth, value);
        }

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand SignUpCommand { get; }

        public SignUpViewModel(IUserService userService)
        {
            _userService = userService;   
            DateOfBirth = DateTime.Today.AddYears(-16); 
            SignUpCommand = new Command(async()=>await OnSignUp());
        }

        private async Task OnSignUp()
        {
            if (Password != ConfirmPassword)
            {
                // Display an error message
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Email = Email,
                Password = Password
            };

            await _userService.SignUpAsync(user);
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
