using Prism.Commands;
using Prism.Navigation;
using Plugin.Connectivity;
using Prism.Services;
using System.Threading.Tasks;
using PrismBarbearia.Services;
using PrismBarbearia.Helpers;

namespace PrismBarbearia.ViewModels
{
    public class MenuPageViewModel : BaseViewModel 
    {
        //servico de alertas
        IPageDialogService _pageDialogService;
        //servico do azure
        AzureService azureService;
        public DelegateCommand LoginFacebookCommand { get; private set; }
        public DelegateCommand LogOutFacebookCommand { get; private set; }

        private bool isVisibleLogInButton;
        public bool IsVisibleLogInButton
        {
            get { return isVisibleLogInButton; }
            set { SetProperty(ref isVisibleLogInButton, value); }
        }

        private bool isVisibleLogOutButton;
        public bool IsVisibleLogOutButton
        {
            get { return isVisibleLogOutButton; }
            set { SetProperty(ref isVisibleLogOutButton, value); }
        }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public MenuPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Agendar";

            //instanciando servico de alertas
            _pageDialogService = pageDialogService;
            IsVisibleLogInButton = CrossConnectivity.Current.IsConnected;
            IsVisibleLogOutButton = false;

            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;
            azureService = Xamarin.Forms.DependencyService.Get<AzureService>();
            LoginFacebookCommand = new DelegateCommand(async () => await ExecuteLoginFacebookCommand());
            LogOutFacebookCommand = new DelegateCommand(async () => await ExecuteLogOutFacebookCommand());
        }

        private async Task ExecuteLoginFacebookCommand()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (IsBusy || !(await LoginAsync()))
                    return;

                else
                {
                    //await _navigationService.NavigateAsync("ServicesPage", null, false);
                }
                IsBusy = false;
                IsVisibleLogInButton = false;
                IsVisibleLogOutButton = true;
            }
            else //Se desconectado
            {
                await _pageDialogService.DisplayAlertAsync("Sem rede", "não é possível realizar agendamentos sem conexão com a internet", "OK");
            }
        }

        private async Task ExecuteLogOutFacebookCommand()
        {
            if (Settings.IsLoggedIn)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                IsVisibleLogInButton = true;
                IsVisibleLogOutButton = false;
                //TODO voltar para página inicial para fazer login
                //if (está na pagina de serviços) entao await _navigationService.GoBackAsync();
            }
        }

        private Task<bool> LoginAsync()
        {
            IsBusy = true;

            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return azureService.LoginAsync();
        }
    }
}
