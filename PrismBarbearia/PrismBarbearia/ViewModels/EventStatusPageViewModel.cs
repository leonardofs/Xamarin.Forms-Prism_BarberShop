using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using PrismBarbearia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class EventStatusPageViewModel : BaseViewModel, INavigatedAware
    {
        public ObservableCollection<BarberSchedule> BarberSchedulesList { get; }

        private string nomeCliente;
        public string NomeCliente
        {
            get { return nomeCliente; }
            set { SetProperty(ref nomeCliente, value); }
        }

        private string telefoneCliente;
        public string TelefoneCliente
        {
            get { return telefoneCliente; }
            set { SetProperty(ref telefoneCliente, value); }
        }

        private string serviço;
        public string Serviço
        {
            get { return serviço; }
            set { SetProperty(ref serviço, value); }
        }

        public DelegateCommand DesmarcarButtonCommand { get; set; }
        public DelegateCommand CancelarButtonCommand { get; set; }

        private DateTime dateTimeTapped { get; set; }

        private AzureDataService azureDataService;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public EventStatusPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Informações do agendamento";
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();
            DesmarcarButtonCommand = new DelegateCommand(DesmarcarButtonCommandExecute);
            CancelarButtonCommand = new DelegateCommand(CancelarButtonCommandExecute);
        }
        

        public override void OnNavigatedTo(NavigationParameters navigationParams)
        {
            dateTimeTapped = navigationParams.GetValue<DateTime>("dateTime");
            SyncSchedules();
        }

        private async void CancelarButtonCommandExecute()
        {
            await _navigationService.GoBackAsync();
        }

        private async void DesmarcarButtonCommandExecute()
        {
            if (!IsBusy)
            {
                Exception Error = null;

                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetSchedule();
                    foreach (var Schedule in Items)
                    {
                        if (Schedule.DateTime == dateTimeTapped)
                        {
                            bool r = await _pageDialogService.DisplayAlertAsync("Desmarcar", "Tem certeza que deseja desmarcar este agendamento", "Sim", "Não");

                            if (r)
                            {
                                await azureDataService.RemoveSchedule(Schedule.Id);
                                await _navigationService.GoBackAsync();
                            }
                            
                        }
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

        async void SyncSchedules()
        {
            if (!IsBusy)
            {
                Exception Error = null;

                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetSchedule();
                    foreach (var Schedule in Items)
                    {
                        if (Schedule.DateTime == dateTimeTapped)
                        {
                            NomeCliente = Schedule.Name;
                            TelefoneCliente = Schedule.PhoneNumber;
                            Serviço = Schedule.Service;
                        }
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
