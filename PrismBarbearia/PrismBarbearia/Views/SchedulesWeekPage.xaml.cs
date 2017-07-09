using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using PrismBarbearia.ViewModels;

namespace PrismBarbearia.Views
{
    public partial class SchedulesWeekPage : ContentPage
    {
        private SchedulesWeekPageViewModel ViewModel => BindingContext as SchedulesWeekPageViewModel;

        public SchedulesWeekPage()
        {
            InitializeComponent();
        }

        private async void CellTappedAsync(object sender, CellTappedEventArgs args)
        {
            if (args.Appointment == null)
            {
                ViewModel.novoEventoAsync(args.Datetime);
            }
            else
            {
                //await ViewModel.cancelarEventoAsync(args.Appointment);
                await ViewModel.cancelarEventoAsync(args.Datetime);
            }

        }

    }
}
