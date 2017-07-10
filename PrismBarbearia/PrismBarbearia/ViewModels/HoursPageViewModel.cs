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
using PrismBarbearia.Helpers;

namespace PrismBarbearia.ViewModels
{
    public class HoursPageViewModel : BaseViewModel, INavigatedAware
    {
        public ObservableCollection<string> Hours { get; }
        public ObservableCollection<BarberHour> HoursAvaliable { get; }
        public ObservableCollection<BarberSchedule> Schedules { get; }
        public ObservableCollection<BarberSchedule> Temp { get; set; }
        private BarberDay dayTapped;
        private BarberService serviceTapped;
        private BarberSchedule scheduleTemp;
        private BarberHour hourSchedule;
        private AzureDataService scheduleService;

        public HoursPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            scheduleService = new AzureDataService();
            Hours = new ObservableCollection<string>();
            Schedules = new ObservableCollection<BarberSchedule>();
            HoursAvaliable = new ObservableCollection<BarberHour>();
            Temp = new ObservableCollection<BarberSchedule>();
            Title = "Horários";
            dayTapped = new BarberDay();
            serviceTapped = new BarberService();
            scheduleTemp = new BarberSchedule();
            hourSchedule = new BarberHour();
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
                if (listItem.DateTime.Date.ToString("dd-MM-yyyy") == dayTapped.Date)
                {
                    Temp.Add(listItem);
                }
            }

            await SyncHours();
            int i = 0, index = 0;
            while (i < Temp.Count)
            {

                scheduleTemp = Temp.ElementAt<BarberSchedule>(i);
                var hora = scheduleTemp.DateTime.Hour;
                string horaTratada;
                if (hora < 10)
                    horaTratada = "0" + hora.ToString();
                else
                    horaTratada = hora.ToString();
                var minutos = scheduleTemp.DateTime.Minute.ToString();
                if (minutos == "0")
                    minutos = "00";
                string horario = horaTratada + ":" + minutos;
                index = Hours.IndexOf(horario);
                if (index >= 0)
                {
                    Hours.RemoveAt(index);
                    HoursAvaliable.RemoveAt(index);
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
                        Hours.Add(Hour.Hour);
                        HoursAvaliable.Add(Hour);
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
                    var Items = await Repository.GetSchedule(/*dayTapped.Date*/);
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
            if (hourTapped != null)
            {
                if (Settings.IsLoggedIn)
                {
                    BarberHour _hourTapped = hourTapped as BarberHour;

                    DateTime scheduleDate = DateTime.ParseExact((dayTapped.Date + " " + _hourTapped.Hour), "dd-MM-yyyy HH:mm",
                                                           System.Globalization.CultureInfo.InvariantCulture);

                    await scheduleService.AddSchedule(serviceTapped.ServiceName, scheduleDate);

                    await _pageDialogService.DisplayAlertAsync("Agendamento", "Agendado com sucesso:" +
                                                               "\nServiço: " + serviceTapped.ServiceName +
                                                               "\nData: " + scheduleDate, "OK");
                    await _navigationService.GoBackAsync(null, false);
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Faça o LogIn", "Para realizar o agendamento é preciso estar logado", "OK");
                }
            }

        }

    }
}
