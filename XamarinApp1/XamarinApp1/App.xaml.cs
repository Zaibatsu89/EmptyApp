using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinApp1.Services;
using XamarinApp1.Views;

namespace XamarinApp1
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
    }
}
