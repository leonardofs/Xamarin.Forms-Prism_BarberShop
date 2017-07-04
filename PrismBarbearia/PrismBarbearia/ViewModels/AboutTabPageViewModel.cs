using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class AboutTabPageViewModel : BaseViewModel
    {
        public AboutTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "A BARBEARIA";
        }
    }
}
