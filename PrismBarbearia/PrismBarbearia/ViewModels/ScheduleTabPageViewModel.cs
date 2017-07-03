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
        //public BarberService cortarCabelo;
        //public BarberService fazerBarba;
        //public BarberService pintarCabelo;
        //public BarberService tirarPraLavar;
        protected AzureDataService azureDataService;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ScheduleTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();

            Title = "AGENDAR";
            BarberServicesList = new ObservableCollection<BarberService>();
            SyncServices();
            //cortarCabelo = new BarberService();
            //cortarCabelo.Name = "Cortar cabelo";
            //cortarCabelo.Price = "20,00";

            //fazerBarba = new BarberService();
            //fazerBarba.Name = "Fazer barba";
            //fazerBarba.Price = "10,00";

            //pintarCabelo = new BarberService();
            //pintarCabelo.Name = "Pintar cabelo";
            //pintarCabelo.Price = "30,00";

            //tirarPraLavar = new BarberService();
            //tirarPraLavar.Name = "Tirar pra lavar";
            //tirarPraLavar.Price = "90,00";


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
                    await Application.Current.MainPage.DisplayAlert("Erro", Error.Message, "OK");
                }
            }
            return;
        }

        public async void Navigate(object serviceTapped)
        {
            if(serviceTapped != null)
            {
                BarberService service = serviceTapped as BarberService;
                await azureDataService.AddService("id63", service.ServiceName, "R$ "+service.ServicePrice);
                await _navigationService.NavigateAsync("DaysPage", serviceTapped as NavigationParameters, false);
                SyncServices();
            }                    
        }

    }
}