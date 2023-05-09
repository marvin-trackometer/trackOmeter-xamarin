using trackOmeter.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace trackOmeter.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command CreateAccountCommand { get; }
        public Command ResetPasswordCommand { get; }
        public Command SettingsCommand { get; }
        public Command HelpCommand { get; }


        public LoginViewModel()
        {
            Title = "Sign In";
            LoginCommand = new Command(OnLoginClicked);
            CreateAccountCommand = new Command(OnCreateAccountClicked);
            ResetPasswordCommand = new Command(OnResetPasswordClicked);
            SettingsCommand = new Command(OnSettingsClicked);
            HelpCommand = new Command(async () => await Browser.OpenAsync("https://gps.trackometer.net/en-us/login"));
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        private async void OnCreateAccountClicked(object obj)
        {
            await Shell.Current.GoToAsync($"createAccount");
        }
        private async void OnResetPasswordClicked(object obj)
        {
            await Shell.Current.GoToAsync($"resetPassword");
        }
        private async void OnSettingsClicked(object obj)
        {
            await Shell.Current.GoToAsync($"settings");
        }
    }
}
