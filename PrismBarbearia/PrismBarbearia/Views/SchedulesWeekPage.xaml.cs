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

            if (Device.OS != TargetPlatform.Android)
            {
                schedule.ScheduleView = ScheduleView.WorkWeekView;
                //Create new instance of NonAccessibleBlock
                NonAccessibleBlock nonAccessibleBlock = new NonAccessibleBlock();
                //Create new instance of NonAccessibleBlocksCollection
                NonAccessibleBlocksCollection nonAccessibleBlocksCollection = new NonAccessibleBlocksCollection();
                nonAccessibleBlock.StartTime = 11;
                nonAccessibleBlock.EndTime = 13;
                nonAccessibleBlock.Text = "ALMOÇO";
                nonAccessibleBlock.Color = Color.DarkGreen;
                nonAccessibleBlocksCollection.Add(nonAccessibleBlock);
                schedule.WorkWeekViewSettings.NonAccessibleBlocks = nonAccessibleBlocksCollection;
            }
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
