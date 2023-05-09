using trackOmeter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trackOmeter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResetPasswordPage : ContentPage
	{
		public ResetPasswordPage ()
		{
			InitializeComponent();
            this.BindingContext = new ResetPasswordModel();
        }
	}
}