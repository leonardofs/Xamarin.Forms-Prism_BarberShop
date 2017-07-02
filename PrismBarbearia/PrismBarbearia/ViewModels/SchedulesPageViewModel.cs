using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    {
        public ObservableCollection<BarberService> BarberServicesList { get; }
        public BarberService cortarCabelo;
        public BarberService fazerBarba;
        public BarberService pintarCabelo;
        public BarberService tirarPraLavar;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public SchedulesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
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

    }
}