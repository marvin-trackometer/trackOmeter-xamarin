using trackOmeter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trackOmeter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubscriptionPage : ContentPage
	{
		public SubscriptionPage()
		{
            InitializeComponent();
			this.BindingContext = new SubscriptionViewModel();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //navigationPage.BarBackgroundColor = Color.Black;
        }
    }
}