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
        private readonly IArticleService _articleService;

        public LoadingPageViewModel(INavigationService navService, TokenContainer tokenContainer, IArticleService articleService)
        {
            _navService = navService;
            _tokenContainer = tokenContainer;
            _articleService = articleService;
        }

        public async void OnAppearing()
        {
            await _tokenContainer.LoadFromRepoAsync();

            string startPage;
            if (_tokenContainer.IdentityToken.IsValid)
            {
                startPage = $"/{nameof(NavigationPage)}/{nameof(MainPage)}";
                await _articleService.GetBaseData();
            }
            else
            {
                startPage = $"/{nameof(LoginPage)}";
            }

            await _navService.NavigateAsync(startPage).ConfigureAwait(false);
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
