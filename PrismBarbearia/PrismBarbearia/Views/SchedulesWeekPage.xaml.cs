using PrismBarbearia.Models;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.ObjectModel;
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
        }

        public void CellTapped(object sender, DateTime dateTime/*, BarberShopAppointment pintarCabelo*/)
        {
            Debug.WriteLine(dateTime);
        }        
    }
}
