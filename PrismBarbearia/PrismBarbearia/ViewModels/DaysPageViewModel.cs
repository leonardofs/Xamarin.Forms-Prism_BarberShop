using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class DaysPageViewModel : BaseViewModel
    {
        public DaysPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Dias";
        }
    }
}
