using Syncfusion.SfSchedule.XForms;
using System;
using System.Diagnostics;
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
            WorkWeekViewSettings workweekViewSettings = new WorkWeekViewSettings();
            WorkWeekLabelSettings workWeekLabelSettings = new WorkWeekLabelSettings();
            workWeekLabelSettings.TimeFormat = "hh:mm";
            workWeekLabelSettings.TimeLabelColor = Color.DarkGreen;
            workweekViewSettings.WorkWeekLabelSettings = workWeekLabelSettings;
            schedule.WorkWeekViewSettings = workweekViewSettings;
        }
        
        public void schedule_CellTapped(object sender, CellTappedEventArgs args)
        {
            //ViewModel.novoEventoPintarCabelo(args.Datetime);
            ViewModel.cancelarEvento(args.Appointment);
        }      

    }
}
