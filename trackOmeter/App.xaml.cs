﻿using trackOmeter.Views;
using Xamarin.Forms;

namespace trackOmeter
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new LoginPage());
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
