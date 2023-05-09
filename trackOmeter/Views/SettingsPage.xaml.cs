using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trackOmeter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trackOmeter.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
			this.BindingContext = new SettingsViewModel();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //navigationPage.BarBackgroundColor = Color.Black;
        }
    }
}