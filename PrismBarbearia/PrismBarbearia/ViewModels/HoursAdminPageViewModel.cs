using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using PrismBarbearia.Models;
using System.Threading.Tasks;
using PrismBarbearia.Services;
using PrismBarbearia.Helpers;
using Plugin.Connectivity;
using Prism.Commands;

namespace PrismBarbearia.ViewModels
{
    public class HoursAdminPageViewModel : BaseViewModel, INavigatedAware
    {
        private string nomeEntry;
        public string NomeEntry
        {
            get { return nomeEntry; }
            set
            {
                SetProperty(ref nomeEntry, value);
                canExecuteAgendarButtonChanged();
            }
        }

        private string telefoneEntry;
        public string TelefoneEntry
        {
            get { return telefoneEntry; }
            set
            {
                SetProperty(ref telefoneEntry, value);
                canExecuteAgendarButtonChanged();
            }
        }

        private BarberHour selectedHour;
        public BarberHour SelectedHour
        {
            get { return selectedHour; }
            set
            {
                SetProperty(ref selectedHour, value);
                canExecuteAgendarButtonChanged();
            }
        }

        private bool canExecuteAgendarButton;
        public bool CanExecuteAgendarButton
        {
            get { return canExecuteAgendarButton; }
            set { SetProperty(ref canExecuteAgendarButton, value); }
        }

        public DelegateCommand AgendarButtonCommand { get; set; }
        public DelegateCommand CancelarButtonCommand { get; set; }

        public ObservableCollection<string> Hours { get; }
        public ObservableCollection<BarberHour> HoursAvaliable { get; }
        public ObservableCollection<BarberSchedule> Schedules { get; }
        public ObservableCollection<BarberSchedule> Temp { get; set; }
        private BarberDay dayTapped;
        private BarberService serviceTapped;
        private BarberSchedule scheduleTemp;
        private AzureDataService scheduleService;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public HoursAdminPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            NomeEntry = "";
            TelefoneEntry = "";
            scheduleService = new AzureDataService();
            Hours = new ObservableCollection<string>();
            Schedules = new ObservableCollection<BarberSchedule>();
            HoursAvaliable = new ObservableCollection<BarberHour>();
            Temp = new ObservableCollection<BarberSchedule>();
            Title = "AGENDAMENTO";
            dayTapped = new BarberDay();
            serviceTapped = new BarberService();
            scheduleTemp = new BarberSchedule();
            AgendarButtonCommand = new DelegateCommand(async () => await ExecuteAgendarButtonCommand());
            CancelarButtonCommand = new DelegateCommand(async () => await ExecuteCancelarButtonCommand());
            CallSync();
        }

        private void canExecuteAgendarButtonChanged()
        {
            if (NomeEntry.Length > 1 && TelefoneEntry.Length > 7 && SelectedHour != null)
                CanExecuteAgendarButton = true;
            else
                CanExecuteAgendarButton = false;
        }

        private async Task ExecuteCancelarButtonCommand()
        {
            await _navigationService.GoBackAsync(null, false);
        }

        private async Task ExecuteAgendarButtonCommand()
        {
            if (SelectedHour != null)
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    DateTime scheduleDate = DateTime.ParseExact((dayTapped.Date + " " + SelectedHour.Hour), "dd-MM-yyyy HH:mm",
                                                           System.Globalization.CultureInfo.InvariantCulture);

                    bool r = await _pageDialogService.DisplayAlertAsync("Agendamento", "Tem certeza que deseja realizar este agendamento?\n" +
                                                               "\nServiço: " + serviceTapped.ServiceName +
                                                               "\nData: " + dayTapped.Date + " " + SelectedHour.Hour, "Sim", "Não");
                    if (r)
                    {
                        await scheduleService.AddSchedule(serviceTapped.ServiceName, NomeEntry, TelefoneEntry, "email não informado", "aniversário não informado", scheduleDate);

                        await _pageDialogService.DisplayAlertAsync("Agendado", "Agendamento realizado com sucesso!", "OK");

                        await _navigationService.GoBackAsync(null, false);
                    }

                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Sem rede", "Não é possível fazer agendamentos sem conexão com a internet", "OK");
                }
            }


        }

        async void CallSync()
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
                    var Items = await Repository.GetSchedule();
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

    }
}
