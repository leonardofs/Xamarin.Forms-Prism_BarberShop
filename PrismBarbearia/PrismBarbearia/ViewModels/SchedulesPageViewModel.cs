using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System.Collections.ObjectModel;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    {
        public ObservableCollection<BarberService> BarberServicesList { get; }
        public BarberService cortarCabelo;
        public BarberService fazerBarba;

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

            BarberServicesList = new ObservableCollection<BarberService> { cortarCabelo, fazerBarba };
        }

    }
}