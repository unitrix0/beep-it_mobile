using Mobile.Abstractions;
using Mobile.Data;
using Mobile.Helpers;
using Mobile.ViewModels;
using Mobile.Views;
using Prism;
using Prism.Ioc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DryIoc;
using Mobile.DTOs;
using Prism.DryIoc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App
    {

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAbsolutAsync(nameof(LoadingPage));
        }

        protected override void RegisterTypes(IContainerRegistry container)
        {
            container.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            container.RegisterSingleton<TokenContainer>();
            container.RegisterSingleton<HttpMessageHandler, BearerTokenHandler>();
            container.RegisterSingleton<ErrorResponseHandler>();
            container.GetContainer().RegisterDelegate<HttpClient>(serviceProvider =>
                new HttpClient(serviceProvider.Resolve<BearerTokenHandler>())
                {
                    BaseAddress = new Uri("http://drone02.hive.loc:5000/api/")
                });

            container.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            container.Register<IArticleService, ArticleService>();

            container.RegisterForNavigation<MainPage>();
            container.RegisterForNavigation<NavigationPage>();
            container.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            container.RegisterForNavigation<ScanPage, ScanPageViewModel>();
            container.RegisterForNavigation<ArticlesPage>();
            container.RegisterForNavigation<SettingsPage>();
            container.RegisterForNavigation<ShoppingListPage>();

            container.RegisterForNavigation<ArticleBasePage, ArticleBasePageViewModel>();
            container.RegisterForNavigation<LoadingPage, LoadingPageViewModel>();
        }
    }
}
