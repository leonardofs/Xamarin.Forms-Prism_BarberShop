using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using PrismBarbearia.ViewModels;
using System.Diagnostics;

namespace PrismBarbearia.Views
{
    public partial class SchedulesWeekPage : ContentPage
    {
        private SchedulesWeekPageViewModel ViewModel => BindingContext as SchedulesWeekPageViewModel;

        public SchedulesWeekPage()
        {
            InitializeComponent();
            WorkWeekViewSettings workweekViewSettings = new WorkWeekViewSettings();
            WorkWeekLabelSettings workWeekLabelSettings = new WorkWeekLabelSettings();
            workWeekLabelSettings.TimeFormat = "hh:mm";
            workWeekLabelSettings.TimeLabelColor = Color.DarkGreen;
            workweekViewSettings.WorkWeekLabelSettings = workWeekLabelSettings;
            schedule.WorkWeekViewSettings = workweekViewSettings;
        }

        private async void CellTappedAsync(object sender, CellTappedEventArgs args)
        {
            if (args.Appointment == null)
            {
                ViewModel.novoEventoAsync(args.Datetime);
                Debug.WriteLine(args.Datetime);
            }
            else
            {
                await ViewModel.cancelarEventoAsync(args.Appointment);
            }

        }

    }
}
