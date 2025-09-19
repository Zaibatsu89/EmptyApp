using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Controls.Xaml;

using XamarinApp1.Models;
using XamarinApp1.ViewModels;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace XamarinApp1.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}