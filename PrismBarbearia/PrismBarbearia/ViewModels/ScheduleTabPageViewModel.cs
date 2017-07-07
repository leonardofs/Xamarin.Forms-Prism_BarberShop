using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System.Collections.ObjectModel;
using PrismBarbearia.Services;
using System;
using Xamarin.Forms;

namespace PrismBarbearia.ViewModels
{
    public class ScheduleTabPageViewModel : BaseViewModel
    {

        public ObservableCollection<BarberService> BarberServicesList { get; }
        protected AzureDataService azureDataService;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ScheduleTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();
            Title = "AGENDAR";
            BarberServicesList = new ObservableCollection<BarberService>();
            SyncServices();
        }

        async void SyncServices()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                BarberServicesList.Clear();
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetServices();
                    foreach (var Service in Items)
                    {
                        BarberServicesList.Add(Service);
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

        public async void Navigate(object serviceTapped)
        {
            if (serviceTapped != null)
            {
                BarberService _serviceTapped = serviceTapped as BarberService;
                //await azureDataService.AddService(_serviceTapped.ServiceName, _serviceTapped.ServicePrice);
                //guarda na easytable, se quiser testar, coloque o nome da sua tabela na Models/BarberService e url no AzureDataService
                
                NavigationParameters navigationParams = new NavigationParameters();
                navigationParams.Add("serviceTapped", serviceTapped);
                await _navigationService.NavigateAsync("DaysPage", navigationParams, false);
            }
        }

    }
}