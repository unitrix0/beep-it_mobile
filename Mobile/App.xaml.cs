using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Services;
using Mobile.Views;

namespace Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void InitializeIcons()
        {
            Plugin.Iconize.Iconize
                .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());            
        }
    }
}
