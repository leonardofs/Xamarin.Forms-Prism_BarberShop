using Xamarin.Forms;
using PrismBarbearia.ViewModels;

namespace PrismBarbearia.Views
{
    public partial class SchedulesPage : ContentPage
    {
        private BaseViewModel ViewModel => BindingContext as BaseViewModel;
        public SchedulesPage()
        {
            InitializeComponent();
        }
    }
}
