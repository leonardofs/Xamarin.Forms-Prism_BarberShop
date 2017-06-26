using PrismBarbearia.Models;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.ObjectModel;
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
        }
    }
}
