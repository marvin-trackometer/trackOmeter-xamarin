using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using trackOmeter.Models;
using trackOmeter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace trackOmeter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        double yHalfPosition;
        double yFullPosition;
        double yZeroPosition;
        int currentPosition;
        double currentPositionY;
        bool up;
        bool down;
        bool isTurnY;
        double valueY;
        double y;
        public MainPage ()
		{
			InitializeComponent();
            this.BindingContext = new MainViewModel();

            MessagingCenter.Subscribe<MainViewModel, HorizontalItem>(this, "ItemSelected", (obj, item) =>
            {
                DisplayAlert("Alert", "You've choosen Item " + item.Title, "cancel");
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["Primary"];

            yHalfPosition = 69;
            yFullPosition = Application.Current.MainPage.Height - 133; // Minus height of shell navbar
            if (Device.RuntimePlatform == Device.iOS)
            {
                yFullPosition = Application.Current.MainPage.Height; // Not sure on IOS yet
            }
            yZeroPosition = 0;
            currentPosition = 0;
            currentPositionY = yZeroPosition;
            slideUpPanelContent.HeightRequest = yHalfPosition;
            slideUpPanel.TranslateTo(slideUpPanel.X, yZeroPosition);
        }

        void PanGestureRecognizer_PanUpdated(Object sender, PanUpdatedEventArgs e)
        {
            // Handle the pan
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't y + e.TotalY pan beyond the wrapped user interface element bounds.
                    var translateY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs((Height * .25) - Height));
                    //slideUpPanel.TranslateTo(slideUpPanel.X, -1*(currentPositionY+(-1*translateY)),20); //up working good

                    if (e.TotalY >= 5 || e.TotalY <= -5 && !isTurnY)
                    {
                        isTurnY = true;
                    }
                    if (isTurnY)
                    {
                        if (e.TotalY <= valueY)
                        {
                            up = true;

                        }
                        if (e.TotalY >= valueY)
                        {
                            down = true;

                        }
                    }
                    if (up)
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            if (yFullPosition < (currentPositionY + (-1 * e.TotalY)))
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yFullPosition);
                            }
                            else
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -1 * (currentPositionY + (-1 * e.TotalY)));
                            }
                        }
                        else
                        {
                            if (yFullPosition < (currentPositionY + (-1 * e.TotalY)))
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yFullPosition, 20);
                            }
                            else
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -1 * (currentPositionY + (-1 * e.TotalY)), 20);
                            }
                        }
                    }
                    else if (down)
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            if (yZeroPosition > currentPositionY - e.TotalY)
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yZeroPosition);
                            }
                            else
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -(currentPositionY - (e.TotalY)));
                            }
                        }
                        else
                        {
                            if (yZeroPosition > currentPositionY - e.TotalY)
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yZeroPosition, 20);
                            }
                            else
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -(currentPositionY - (e.TotalY)), 20);
                            }
                        }
                    }
                    break;
                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    valueY = e.TotalY;
                    y = slideUpPanel.TranslationY;

                    //at the end of the event - snap to the closest location
                    //var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getClosestLockState(e.TotalY + y)));

                    //depending on Swipe Up or Down - change the snapping animation
                    if (up)
                    {
                        //swipe up happened
                        if (currentPosition == 1)
                        {
                            slideUpPanel.TranslateTo(slideUpPanel.X, -yFullPosition);
                            currentPosition = 2;
                            currentPositionY = yFullPosition;
                            slideUpPanelContent.HeightRequest = yFullPosition;
                            slideUpPanelContent.HeightRequest = yFullPosition;
                            timelineContent.IsVisible = false;
                            deviceContent.IsVisible = true;
                            //slideUpPanel.TranslateTo(slideUpPanel.X, finalTranslation, 250, Easing.SpringIn);
                        }
                        else if (currentPosition == 0)
                        {
                            double currentY = (-1) * y;
                            double differentBetweenHalfAndCurrent = Math.Abs(currentY - yHalfPosition);
                            double differentBetweenFullAndCurrent = Math.Abs(currentY - yFullPosition);
                            //check which is close snap point and move to the closest snap point
                            if (differentBetweenHalfAndCurrent < differentBetweenFullAndCurrent)
                            {
                                //yHalfPosition is the closest one
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yHalfPosition);
                                currentPosition = 1;
                                currentPositionY = yHalfPosition;
                                //slideUpPanelContent.HeightRequest = yHalfPosition;
                                deviceContent.IsVisible = false;
                                timelineContent.IsVisible = true;
                            }
                            else
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yFullPosition);
                                currentPosition = 2;
                                currentPositionY = yFullPosition;
                                //slideUpPanelContent.HeightRequest = yFullPosition;
                                timelineContent.IsVisible = false;
                                deviceContent.IsVisible = true;
                            }

                        }

                    }
                    if (down)
                    {
                        //swipe down happened
                        if (currentPosition == 1)
                        {
                            slideUpPanel.TranslateTo(slideUpPanel.X, -yZeroPosition);
                            currentPosition = 0;
                            currentPositionY = yZeroPosition;
                        }
                        else if (currentPosition == 2)
                        {
                            double currentY = (-1) * y;
                            double differentBetweenHalfAndCurrent = Math.Abs(currentY - yHalfPosition);
                            double differentBetweenZeroAndCurrent = Math.Abs(currentY - yZeroPosition);
                            //check which is close snap point and move to the closest snap point
                            if (differentBetweenHalfAndCurrent < differentBetweenZeroAndCurrent)
                            {
                                //yHalfPosition is the closest one
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yHalfPosition);
                                currentPosition = 1;
                                currentPositionY = yHalfPosition;
                                slideUpPanelContent.HeightRequest = yHalfPosition;
                                deviceContent.IsVisible = false;
                                timelineContent.IsVisible = true;
                            }
                            else
                            {
                                slideUpPanel.TranslateTo(slideUpPanel.X, -yZeroPosition);
                                currentPosition = 0;
                                currentPositionY = yZeroPosition;
                                timelineContent.IsVisible = false;
                                deviceContent.IsVisible = true;
                            }


                        }
                        //slideUpPanel.TranslateTo(slideUpPanel.X, finalTranslation, 250, Easing.SpringOut);
                    }

                    //dismiss the keyboard after a transition
                    //SearchBox.Unfocus();
                    y = slideUpPanel.TranslationY;
                    up = false;
                    down = false;
                    break;
            }
        }

        private async void OnShareUriClicked(object sender, EventArgs e)
        {
            var page = new ShareUriPage();
            await PopupNavigation.Instance.PushAsync(page);
        }

    }
}