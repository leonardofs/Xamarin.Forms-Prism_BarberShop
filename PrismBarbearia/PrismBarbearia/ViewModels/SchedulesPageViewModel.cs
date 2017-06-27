using Prism.Commands;
using Prism.Navigation;
using Plugin.Connectivity;
using Prism.Services;
using System.Threading.Tasks;
using PrismBarbearia.Services;
using PrismBarbearia.Helpers;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    {
        //servico de alertas
        IPageDialogService _pageDialogService;

        public DelegateCommand SchedulesWeekPageCommand { get; private set; }
        

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public SchedulesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Agendar";

            //instanciando servico de alertas
            _pageDialogService = pageDialogService;
            SchedulesWeekPageCommand = new DelegateCommand(async () => await ExecuteSchedulesWeekPageCommand());
        }

        private async Task ExecuteSchedulesWeekPageCommand()
        {
            if (Settings.IsLoggedIn)
            {
                    await _navigationService.NavigateAsync("SchedulesWeekPage", null, false);                
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Fazer login", "não é possível realizar agendamentos sem antes ter feito login", "OK");  
            }
        }
    }
}