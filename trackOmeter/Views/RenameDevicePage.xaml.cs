﻿using System;
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
    public partial class RenameDevicePage : ContentPage
    {
        public RenameDevicePage()
        {
            InitializeComponent();
            this.BindingContext = new RenameDeviceViewModel();
        }
    }
}