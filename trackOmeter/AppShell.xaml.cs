using System.Collections.Generic;
using System;
using Xamarin.Forms;
using trackOmeter.Views;

namespace trackOmeter
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        
        /**
         * I've encountered some issues in design using Shell
         * I'm not using this for now :(
         */
        public AppShell() 
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add("createAccount", typeof(CreateAccountPage));
            Routes.Add("resetPassword", typeof(ResetPasswordPage));
            Routes.Add("subscription", typeof(SubscriptionPage));
            Routes.Add("settings", typeof(SettingsPage));
            Routes.Add("renameDevice", typeof(RenameDevicePage));
            Routes.Add("shareDeviceLocation", typeof(ShareDeviceLocationPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
