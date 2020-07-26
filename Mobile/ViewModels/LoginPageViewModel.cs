using Mobile.Abstractions;
using Mobile.Views;
using System;
using System.Windows.Input;
using Mobile.Helpers;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class LoginPageViewModel : TabPageBaseViewModel
    {
        private readonly IAuthenticationService _authService;
        private readonly IPageDialogService _page;
        private string _userName;
        private string _password;
        public ICommand LoginCmd { get; }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public LoginPageViewModel() : base()
        {
        }

        public LoginPageViewModel(INavigationService navService, IAuthenticationService authService, IPageDialogService page)
        : base(navService)
        {
            _authService = authService;
            _page = page;

            LoginCmd = new Command(Login);
        }

        private async void Login()
        {
            try
            {
                await _authService.Login(UserName, Password);
                await NavigationService.NavigateAbsolutAsync(nameof(NavigationPage),nameof(MainPage));
            }
            catch (Exception exception)
            {
                await _page.DisplayAlertAsync("Login fehlgeschlagen", exception.Message, "Schliessen");
            }
        }
    }
}
