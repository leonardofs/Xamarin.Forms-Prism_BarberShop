using Prism.Navigation;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PrismBarbearia.Models;
using System;
using Prism.Services;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesWeekPageViewModel : BaseViewModel, INavigatedAware
    {
        private ObservableCollection<BarberShopAppointment> eventsCollection;
        public ObservableCollection<BarberShopAppointment> EventsCollection
        {
            get { return eventsCollection; }
            set { SetProperty(ref eventsCollection, value); }
        }
        public ObservableCollection<BarberSchedule> BarberSchedulesList { get; }

        private BarberShopAppointment eventAppointment { get; set; }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public SchedulesWeekPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "AGENDA";
            eventAppointment = new BarberShopAppointment();
            EventsCollection = new ObservableCollection<BarberShopAppointment>();
            BarberSchedulesList = new ObservableCollection<BarberSchedule>();
        }

        public async void novoEventoAsync(DateTime dateTime)
        {
            if (dateTime != null)
            {
                NavigationParameters navigationParams = new NavigationParameters();
                navigationParams.Add("dateTime", dateTime);
                await _navigationService.NavigateAsync("NewEventPage", navigationParams, false);                
            }                                        
        }
        
        public async Task cancelarEventoAsync(DateTime dateTime)
        {
            NavigationParameters navigationParams = new NavigationParameters();
            navigationParams.Add("dateTime", dateTime);
            await _navigationService.NavigateAsync("EventStatusPage", navigationParams, false);
            
            //bool r = await _pageDialogService.DisplayAlertAsync("Cancelar evento", "Deseja cancelar este evento?", "Sim", "Não");
            //if (r) EventsCollection.Remove(evento as BarberShopAppointment);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(CrossConnectivity.Current.IsConnected)
            SyncSchedules();
        }

        async void SyncSchedules()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                BarberSchedulesList.Clear();
                EventsCollection.Clear();

                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetSchedule();
                    foreach (var Schedule in Items)
                    {
                        BarberSchedulesList.Add(Schedule);

                        eventAppointment.From = Schedule.DateTime;
                        eventAppointment.To = eventAppointment.From.AddHours(0.5);
                        eventAppointment.EventName = Schedule.Service;
                        eventAppointment.Color = Color.ForestGreen;
                        
                        EventsCollection.Add(eventAppointment);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }
                if (Error != null)
                {
                    await _pageDialogService.DisplayAlertAsync("Erro", Error.Message, "OK");
                }
            }
            return;
        }

    }
}

/* private ScheduleAppointmentCollection scheduleAppointmentCollection; GAMBIARRA PRA TER HORARIO DE ALMOCO NO ANDROID E COME PERFORMANCE '-'
       public ScheduleAppointmentCollection ScheduleAppointmentCollection
       {
           get { return scheduleAppointmentCollection; }
           set { SetProperty(ref scheduleAppointmentCollection, value); }
       }

           scheduleAppointmentCollection = new ScheduleAppointmentCollection();
           var scheduleAppointment = new ScheduleAppointment()
           {
               StartTime = new DateTime(2017, 06, 26, 11, 0, 0),
               EndTime = new DateTime(2017, 06, 26, 13, 0, 0),
               Subject = "Almoço",
               IsRecursive = true
           };
           //Adding schedule appointment in schedule appointment collection
           scheduleAppointmentCollection.Add(scheduleAppointment);

           //Adding schedule appointment collection to DataSource of SfSchedule

           // Creating recurrence rule
           RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
           recurrenceProperties.RecurrenceType = RecurrenceType.Daily;
           recurrenceProperties.IsRangeRecurrenceCount = true;
           recurrenceProperties.DailyNDays = 1;
           recurrenceProperties.IsDailyEveryNDays = true;
           recurrenceProperties.RangeRecurrenceCount = 365*3;
           recurrenceProperties.RecurrenceRule = DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, scheduleAppointment.StartTime, scheduleAppointment.EndTime);

           // Setting recurrence rule to schedule appointment
           scheduleAppointment.RecurrenceRule = recurrenceProperties.RecurrenceRule;*/
