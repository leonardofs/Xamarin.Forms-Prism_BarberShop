using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "BARBEARIA 8-BALL";
        }
    }
}