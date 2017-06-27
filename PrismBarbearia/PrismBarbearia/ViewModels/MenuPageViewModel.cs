using Prism.Commands;
using Prism.Navigation;
using Plugin.Connectivity;
using Prism.Services;
using System.Threading.Tasks;
using PrismBarbearia.Services;
using PrismBarbearia.Helpers;
using System.Diagnostics;

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

        private bool isVisibleAdminButtons;
        public bool IsVisibleAdminButtons
        {
            get { return isVisibleAdminButtons; }
            set { SetProperty(ref isVisibleAdminButtons, value); }
        }

        private bool isVisibleUserButtons;
        public bool IsVisibleUserButtons
        {
            get { return isVisibleUserButtons; }
            set { SetProperty(ref isVisibleUserButtons, value); }
        }
        
        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public MenuPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Agendar";

            //instanciando servico de alertas
            _pageDialogService = pageDialogService;

            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            IsVisibleLogInButton = !Settings.IsLoggedIn;
            IsVisibleLogOutButton = Settings.IsLoggedIn;
            IsVisibleAdminButtons = Settings.IsAdmin;
            IsVisibleUserButtons = !Settings.IsAdmin;

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
                if (Settings.IsAdmin)
                    IsVisibleAdminButtons = true;
                    IsVisibleUserButtons = false;

            }
            else //Se desconectado
            {
                await _pageDialogService.DisplayAlertAsync("Sem rede", "não é possível fazer login sem conexão com a internet", "OK");
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
                IsVisibleAdminButtons = false;
                IsVisibleUserButtons = true;
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
