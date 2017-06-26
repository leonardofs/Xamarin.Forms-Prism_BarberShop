using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PrismBarbearia.Models;
using System;
using PrismBarbearia.Views;
using System.Diagnostics;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesWeekPageViewModel : BaseViewModel
    {
        private ObservableCollection<BarberShopAppointment> eventsCollection;
        public ObservableCollection<BarberShopAppointment> EventsCollection
        {
            get { return eventsCollection; }
            set { SetProperty(ref eventsCollection, value); }
        }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public SchedulesWeekPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            //-------------------------------------------------TESTES--------------------------------------------------//
            // Creating instance for custom appointment class
            BarberShopAppointment cortarCabelo = new BarberShopAppointment();
            BarberServices servico = new BarberServices();
            // Setting start time of an event
            cortarCabelo.From = new DateTime(2017, 06, 28, 10, 0, 0);
            // Setting end time of an event
            cortarCabelo.To = cortarCabelo.From.AddHours(0.5);//30 minutos de duração
            // Setting start time for an 
            servico.Name = "cortar cabelo";
            cortarCabelo.EventName = servico.Name;
            // Setting color for an event
            cortarCabelo.Color = Color.Green;
            // Creating instance for collection of custom appointments
            eventsCollection = new ObservableCollection<BarberShopAppointment>();
            // Adding a custom appointment in CustomAppointmentCollection
            eventsCollection.Add(cortarCabelo);
            // Adding custom appointments in DataSource of SfSchedule       

            BarberShopAppointment fazerBarba = new BarberShopAppointment();
            fazerBarba.From = new DateTime(2017, 06, 29, 10, 0, 0);
            fazerBarba.To = fazerBarba.From.AddHours(0.5);
            servico.Name = "fazer barba";
            fazerBarba.EventName = servico.Name;
            fazerBarba.Color = Color.Blue;
            eventsCollection.Add(fazerBarba);

            BarberShopAppointment pintarCabelo = new BarberShopAppointment();
            pintarCabelo.From = new DateTime(2017, 06, 29, 11, 0, 0);
            pintarCabelo.To = pintarCabelo.From.AddHours(1);
            servico.Name = "pintar cabelo";
            pintarCabelo.EventName = servico.Name;
            pintarCabelo.Color = Color.Pink;
            eventsCollection.Add(pintarCabelo);         
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
