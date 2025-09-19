using System.ComponentModel;

using Xamarin.Forms;

using XamarinApp1.ViewModels;

namespace XamarinApp1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}