using PrismBarbearia.ViewModels;
using Xamarin.Forms;

namespace PrismBarbearia.Views
{
    public partial class HoursPage : ContentPage
    {
        private HoursPageViewModel ViewModel => BindingContext as HoursPageViewModel;

        public HoursPage()
        {
            InitializeComponent();
        }

        private void HourTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ViewModel.NewSchedule(e.ItemData);
        }
    }
}
