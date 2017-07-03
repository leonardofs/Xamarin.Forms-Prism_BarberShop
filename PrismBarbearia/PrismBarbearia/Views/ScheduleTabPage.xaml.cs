using PrismBarbearia.ViewModels;
using Xamarin.Forms;

namespace PrismBarbearia.Views
{
    public partial class ScheduleTabPage : ContentPage
    {
        private ScheduleTabPageViewModel ViewModel => BindingContext as ScheduleTabPageViewModel;

        public ScheduleTabPage()
        {
            InitializeComponent();
        }

        private void services_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ViewModel.Navigate(e.ItemData);
        }
    }
}
