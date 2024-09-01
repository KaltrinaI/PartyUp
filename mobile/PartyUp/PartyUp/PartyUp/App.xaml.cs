using System;
using Autofac;
using Autofac.Core;
using PartyUp.Services;
using PartyUp.ViewModels;
using PartyUp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PartyUp
{
    public partial class App : Application
    {
        private static ContainerBuilder _containerBuilder = new ContainerBuilder();
        public static IContainer Container;
        public App()
        {
            InitializeComponent();
            RegisterViewModels();
            RegisterServices();
            Container = _containerBuilder.Build();
            MainPage = new NavigationPage(new LoginPage());
        }

        private void RegisterViewModels()
        {
            _containerBuilder.RegisterType<BaseViewModel>();
            _containerBuilder.RegisterType<EventDetailsViewModel>();
            _containerBuilder.RegisterType<ExploreEventsViewModel>();
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<MyUpcomingEventsViewModel>();
            _containerBuilder.RegisterType<SignUpViewModel>();
            _containerBuilder.RegisterType<ProfileViewModel>();
            _containerBuilder.RegisterType<CommentsViewModel>();
        }

        private void RegisterServices()
        {
            _containerBuilder.RegisterType<BusinessEntityService>().As<IBusinessEntityService>();
            _containerBuilder.RegisterType<EventService>().As<IEventService>();
            _containerBuilder.RegisterType<HttpService>().As<IHttpService>();
            _containerBuilder.RegisterType<UserService>().As<IUserService>();
            _containerBuilder.RegisterType<CommentsService>().As<ICommentsService>();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
