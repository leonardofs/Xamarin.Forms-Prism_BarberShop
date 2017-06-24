using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;

namespace PrismBarbearia.Views
{
    public partial class ScheduleWeek : ContentPage
    {
        public ScheduleWeek()
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
