using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PrismBarbearia.Models;
using System.Threading.Tasks;
using PrismBarbearia.Services;
namespace PrismBarbearia.ViewModels
{
    public class HoursPageViewModel : BaseViewModel, INavigatedAware
    {
        public ObservableCollection<BarberHour> Hours { get; }
        public ObservableCollection<BarberSchedule> Schedules { get; }
        public ObservableCollection<BarberSchedule> Temp { get; set; }
        public ObservableCollection<BarberHour> HoursTemp { get; }
        private BarberDay dayTapped;
        private BarberService serviceTapped;
        private BarberSchedule scheduleTemp;
        private BarberHour hourSchedule;
        AzureDataService scheduleService;
        public HoursPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            scheduleService = new AzureDataService();
            Hours = new ObservableCollection<BarberHour>();
            Schedules = new ObservableCollection<BarberSchedule>();
            Temp = new ObservableCollection<BarberSchedule>();
            HoursTemp = new ObservableCollection<BarberHour>();
            Title = "Horários";
            dayTapped = new BarberDay();
            serviceTapped = new BarberService();
            scheduleTemp = new BarberSchedule();
            hourSchedule = new BarberHour();
            hourSchedule.Hour = "00:00";
            CallSyncAvaliableHours();

        }

        async void CallSyncAvaliableHours()
        {
            await SyncAvaliableHours();
        }

        async Task SyncAvaliableHours()
        {
            await SyncSchedules();
            var schedulesAz = Schedules;
            foreach (var item in schedulesAz)
            {
                var listItem = item as BarberSchedule;
                if (listItem.Date == dayTapped.Date)
                {
                    Temp.Add(listItem);
                }
            }

            await SyncHours();
            int i = 0,index = 1;
            while(i < Temp.Count)
            {

                scheduleTemp = Temp.ElementAt<BarberSchedule>(i);
                hourSchedule.Hour = scheduleTemp.Hour;
                //index = Hours.IndexOf(hourSchedule);
                if (index >= 0)
                {
                    Hours.RemoveAt(index);
                }
                i++;
            }
            return;
        }

        async Task SyncHours()
        {
            if (!IsBusy)
            {
                Exception Error = null;

                Hours.Clear();
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetHours();
                    foreach (var Hour in Items)
                    {
                        Hours.Add(Hour);
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
                return;
            }
        }

        async Task SyncSchedules()
        {
            if (!IsBusy)
            {
                Exception Error = null;

                Schedules.Clear();
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetSchedule(dayTapped.Date);
                    foreach (var Service in Items)
                    {
                        Schedules.Add(Service);
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
                return;
            }
        }

        public override void OnNavigatedTo(NavigationParameters navigationParams)
        {
            serviceTapped = navigationParams.GetValue<BarberService>("serviceTapped");
            dayTapped = navigationParams.GetValue<BarberDay>("dayTapped");            
        }

        public async void NewSchedule(object hourTapped)
        {
            //if (hourTapped != null) comentei para clicar em "Escolha um horário no cabeçado" para fazer teste"
            //{
                BarberHour _hourTapped = hourTapped as BarberHour;
                await scheduleService.AddSchedule(serviceTapped.ServiceName, dayTapped.Date, _hourTapped.Hour);
                await _pageDialogService.DisplayAlertAsync("Agendamento", "Agendado com sucesso:" +
                                                           "\nServiço: " + serviceTapped.ServiceName +
                                                           "\nDia: " + dayTapped.Date+
                                                           "\nHorário: " + _hourTapped.Hour, "OK");
            
                //await _navigationService.GoBackAsync(null, false);
            //}
        }
        
    }
}
