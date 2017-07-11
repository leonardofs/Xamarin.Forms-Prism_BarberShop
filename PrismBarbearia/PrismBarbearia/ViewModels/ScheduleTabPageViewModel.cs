using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System.Collections.ObjectModel;
using PrismBarbearia.Services;
using System;
using Xamarin.Forms;
using Plugin.Connectivity;
using PrismBarbearia.Helpers;

namespace PrismBarbearia.ViewModels
{
    public class ScheduleTabPageViewModel : BaseViewModel
    {

        public ObservableCollection<BarberService> BarberServicesList { get; }


        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ScheduleTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "AGENDAR";
            BarberServicesList = new ObservableCollection<BarberService>();
            if (CrossConnectivity.Current.IsConnected)
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
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (Settings.IsLoggedIn)
                    {
                        NavigationParameters navigationParams = new NavigationParameters();
                        navigationParams.Add("serviceTapped", serviceTapped);
                        await _navigationService.NavigateAsync("DaysPage", navigationParams, false);
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Faça o Login", "Para realizar o agendamento é preciso estar logado", "OK");
                    }
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Sem rede", "Não é possível fazer agendamentos sem conexão com a internet", "OK");
                }
            }
        }

    }
}