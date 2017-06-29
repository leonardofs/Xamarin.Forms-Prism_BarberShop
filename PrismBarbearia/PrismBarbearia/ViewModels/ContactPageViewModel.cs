using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class ContactPageViewModel : BaseViewModel
    {
        public ContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "ONDE ESTAMOS";
        }
    }
}