using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;

namespace PrismBarbearia.ViewModels
{
    public class ScheduleWeekViewModel : BaseViewModel
    {
    private SfSchedule schedule;
    public SfSchedule Schedule
    {
        get { return schedule; }
        set { SetProperty(ref schedule, value); }
    }
        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ScheduleWeekViewModel(INavigationService navigationService) : base(navigationService)
        {
            SfSchedule schedule = new SfSchedule();
            schedule.ScheduleView = ScheduleView.WorkWeekView;
            WorkWeekViewSettings workweekViewSettings = new WorkWeekViewSettings();
            WorkWeekLabelSettings workWeekLabelSettings = new WorkWeekLabelSettings();
            workWeekLabelSettings.TimeFormat = "hh mm";
            workWeekLabelSettings.TimeLabelColor = Color.Blue;
            workweekViewSettings.WorkWeekLabelSettings = workWeekLabelSettings;
            schedule.WorkWeekViewSettings = workweekViewSettings;
        }
    }
}
