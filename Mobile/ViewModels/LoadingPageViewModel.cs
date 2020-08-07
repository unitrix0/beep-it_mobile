using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.Helpers;
using Mobile.Views;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class LoadingPageViewModel : BindableBase, IApplicationLifecycleAware, IPageLifecycleAware
    {
        private readonly INavigationService _navService;
        private readonly TokenContainer _tokenContainer;

        public LoadingPageViewModel(INavigationService navService, TokenContainer tokenContainer)
        {
            _navService = navService;
            _tokenContainer = tokenContainer;
        }

        public async void OnAppearing()
        {
            await _tokenContainer.LoadFromRepoAsync();

            string startPage = _tokenContainer.IdentityToken.IsVaid ? nameof(MainPage) : nameof(LoginPage);
            await _navService.NavigateAbsolutAsync($"/{startPage}").ConfigureAwait(false);
        }

        public void OnDisappearing()
        {
        }

        public void OnResume()
        {
            Console.WriteLine("OnResume");
        }

        public void OnSleep()
        {
            Console.WriteLine("OnSleep");
        }
    }
}
