using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System.Collections.ObjectModel;
using PrismBarbearia.Services;

namespace PrismBarbearia.ViewModels
{
    public class ScheduleTabPageViewModel : BaseViewModel
    {
        public ObservableCollection<BarberService> BarberServicesList { get; }
        public BarberService cortarCabelo;
        public BarberService fazerBarba;
        public BarberService pintarCabelo;
        public BarberService tirarPraLavar;
        protected AzureDataService azureDataService;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ScheduleTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();

            Title = "AGENDAR";

            cortarCabelo = new BarberService();
            cortarCabelo.Name = "Cortar cabelo";
            cortarCabelo.Price = "20,00";

            fazerBarba = new BarberService();
            fazerBarba.Name = "Fazer barba";
            fazerBarba.Price = "10,00";

            pintarCabelo = new BarberService();
            pintarCabelo.Name = "Pintar cabelo";
            pintarCabelo.Price = "30,00";

            tirarPraLavar = new BarberService();
            tirarPraLavar.Name = "Tirar pra lavar";
            tirarPraLavar.Price = "90,00";

            BarberServicesList = new ObservableCollection<BarberService> { cortarCabelo, fazerBarba, pintarCabelo, tirarPraLavar};
        }

        public async void Navigate(object serviceTapped)
        {
            if(serviceTapped != null)
            {
                BarberService service = serviceTapped as BarberService;
                await azureDataService.AddService("id52", service.Name, service.Price);
                await _navigationService.NavigateAsync("DaysPage", serviceTapped as NavigationParameters, false);
            }                    
        }

    }
}