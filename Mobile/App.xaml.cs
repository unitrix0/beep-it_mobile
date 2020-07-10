using Mobile.Abstractions;
using Mobile.Data;
using Mobile.Helpers;
using Prism;
using Prism.Ioc;
using Mobile.ViewModels;
using Mobile.Views;
using Prism.DryIoc;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
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

            await NavigationService.NavigateAbsolutAsync(nameof(LoginPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();

            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage,LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ScanPage,ScanPageViewModel>();
            containerRegistry.RegisterForNavigation<ArticlesPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<ShoppingListPage>();

            containerRegistry.RegisterForNavigation<ArticleBasePage, ArticleBasePageViewModel>();
        }
    }
}
