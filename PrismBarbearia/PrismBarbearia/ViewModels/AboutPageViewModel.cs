using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        public AboutPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "A BARBEARIA";
        }
    }
}
