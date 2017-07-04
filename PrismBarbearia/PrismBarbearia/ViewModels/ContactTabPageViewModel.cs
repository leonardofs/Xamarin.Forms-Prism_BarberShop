using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class ContactTabPageViewModel : BaseViewModel
    {
        public ContactTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "ONDE ESTAMOS";
        }
    }
}