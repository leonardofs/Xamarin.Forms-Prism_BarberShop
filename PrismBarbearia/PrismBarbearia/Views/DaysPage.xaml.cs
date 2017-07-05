using PrismBarbearia.ViewModels;
using Xamarin.Forms;

namespace PrismBarbearia.Views
{
    public partial class DaysPage : ContentPage
    {
        private DaysPageViewModel ViewModel => BindingContext as DaysPageViewModel;

        public DaysPage()
        {
            InitializeComponent();
        }

        private void DayTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ViewModel.Navigate(e.ItemData);
        }
    }
}
