using PanCardView.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using trackOmeter.Models;
using trackOmeter.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace trackOmeter.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly ObservableCollection<DeviceLocation> _locations;
        public IEnumerable DeviceLocations => _locations;
        public ICommand UpgradeCommand { get; }
        public ICommand MyAccountCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand HelpCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand RenameDeviceCommand { get; }
        public ICommand ShareDeviceLocationCommand { get; }

        ObservableCollection<WebLink> webLinks = new ObservableCollection<WebLink>();
        public ObservableCollection<WebLink> WebLinks 
        { 
            get =>  webLinks;
            set => SetProperty(ref webLinks, value);
        }
        public IList<DeviceLocation> Devices { get; set; }
        public DeviceLocation FocusedLocation { get; set; }

        private ObservableCollection<HorizontalItem> horizontalItems;
        public ObservableCollection<HorizontalItem> HorizontalItems
        {
            get => horizontalItems;
            set => SetProperty(ref horizontalItems, value);
        }

        private HorizontalItem selectedItem;
        public HorizontalItem SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                ItemSelected();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _currentIndex;
        private int _imageCount = 1078;
        public MainViewModel()
        {
            Title = "trackOmeter";

            _locations = new ObservableCollection<DeviceLocation>()
            {
                new DeviceLocation("Ayala Center Cebu", new Position(10.31754, 123.9035116), true),
                new DeviceLocation("IT Park", new Position(10.3308892, 123.8983073), false),
            };
            FocusedLocation = _locations.Where(location => location.IsFocused).SingleOrDefault();
            UpgradeCommand = new Command(OnUpgradeClicked);
            MyAccountCommand = new Command(async () => await Browser.OpenAsync("https://gps.trackometer.net/en-us/login"));
            SettingsCommand = new Command(OnSettingsClicked);
            HelpCommand = new Command(async () => await Browser.OpenAsync("https://gps.trackometer.net/en-us/login"));
            SignOutCommand = new Command(OnSignOutClicked);
            RenameDeviceCommand = new Command(OnRenameDeviceClicked);
            ShareDeviceLocationCommand = new Command(OnShareDeviceLocationClicked);

            WebLinks = new ObservableCollection<WebLink>()
            {
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
                new WebLink() { Name = "New Web Link", Link = "https://portal.trackometer.net/share/asdfdasfdsa-123asdf-sadf-123asdfdasfdsa-123asdf-sadf-123" },
            };

            HorizontalItems = new ObservableCollection<HorizontalItem>()
            {
                new HorizontalItem() { Title = "One", Icon = "one.png" },
                new HorizontalItem() { Title = "Two", Icon = "two.png" },
                new HorizontalItem() { Title = "Three", Icon = "three.png" },
                new HorizontalItem() { Title = "Four", Icon = "four.png" },
                new HorizontalItem() { Title = "Five", Icon = "five.png" },
                new HorizontalItem() { Title = "Six", Icon = "six.png" },
                new HorizontalItem() { Title = "Seven", Icon = "seven.png" },
                new HorizontalItem() { Title = "Eight", Icon = "eight.png" },
                new HorizontalItem() { Title = "Nine", Icon = "nine.png" },
                new HorizontalItem() { Title = "Ten", Icon = "ten.png" },
            };

            Items = new ObservableCollection<object>
            {
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Red, Title = "First" },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Green, Title = "Second" },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Gold, Title = "Long Title" },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Silver, Title = "4" },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Blue, Title = "5th" }
            };

            PanPositionChangedCommand = new Command(v =>
            {
                if (IsAutoAnimationRunning || IsUserInteractionRunning)
                {
                    return;
                }

                var index = CurrentIndex + (bool.Parse(v.ToString()) ? 1 : -1);
                if (index < 0 || index >= Items.Count)
                {
                    return;
                }
                CurrentIndex = index;
            });

            RemoveCurrentItemCommand = new Command(() =>
            {
                if (!Items.Any())
                {
                    return;
                }
                Items.RemoveAt(CurrentIndex.ToCyclicalIndex(Items.Count));
            });

            GoToLastCommand = new Command(() =>
            {
                CurrentIndex = Items.Count - 1;
            });
        }

        private async void OnUpgradeClicked(object obj)
        {
            await Shell.Current.GoToAsync($"subscription");
        }
        private async void OnSettingsClicked(object obj)
        {
            await Shell.Current.GoToAsync($"settings");
        }
        private async void OnSignOutClicked(object obj)
        {
            if (await Shell.Current?.CurrentPage.DisplayAlert("Do you wish to Sign Out?", "Background GPS tracking will stop.", "OK", "CANCEL"))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                return;
            }
        }

        private async void OnRenameDeviceClicked(object obj)
        {
            await Shell.Current.GoToAsync($"renameDevice");
        }

        private async void OnShareDeviceLocationClicked(object obj)
        {
            await Shell.Current.GoToAsync($"shareDeviceLocation");
        }

        private void ItemSelected()
        {
            MessagingCenter.Send(this, "ItemSelected", SelectedItem);
        }

        public ICommand PanPositionChangedCommand { get; }

        public ICommand RemoveCurrentItemCommand { get; }

        public ICommand GoToLastCommand { get; }

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
            }
        }

        public bool IsAutoAnimationRunning { get; set; }

        public bool IsUserInteractionRunning { get; set; }

        public ObservableCollection<object> Items { get; }

        private string CreateSource()
        {
            var source = $"https://picsum.photos/500/500?image={_imageCount}";
            return source;
        }
    }
}
