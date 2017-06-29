using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class ServicesPageViewModel : BaseViewModel
    {
        public ServicesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "LISTA DE SERVIÇOS";
        }
    }
}