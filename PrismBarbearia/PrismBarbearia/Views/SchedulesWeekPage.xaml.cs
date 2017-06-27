using Syncfusion.SfSchedule.XForms;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PrismBarbearia.Views
{
    public partial class SchedulesWeekPage : ContentPage
    {
        public SchedulesWeekPage()
        {
            InitializeComponent();
            WorkWeekViewSettings workweekViewSettings = new WorkWeekViewSettings();
            WorkWeekLabelSettings workWeekLabelSettings = new WorkWeekLabelSettings();
            workWeekLabelSettings.TimeFormat = "hh:mm";
            workWeekLabelSettings.TimeLabelColor = Color.DarkGreen;
            workweekViewSettings.WorkWeekLabelSettings = workWeekLabelSettings;
            schedule.WorkWeekViewSettings = workweekViewSettings;
            
            schedule.CellTapped += schedule_CellTapped;
        }
        
        public void schedule_CellTapped(object sender, CellTappedEventArgs args)
        {
            Debug.WriteLine(args.Datetime);
        }      

    }
}
