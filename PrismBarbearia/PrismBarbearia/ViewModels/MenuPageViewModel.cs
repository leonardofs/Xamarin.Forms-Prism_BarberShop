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
        protected AzureService azureService;

        public DelegateCommand LoginFacebookCommand { get; set; }
        public DelegateCommand LogOutFacebookCommand { get; set; }

        private string privilegio;
        public string Privilegio
        {
            get { return privilegio; }
            set { SetProperty(ref privilegio, value); }
        }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public MenuPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            azureService = Xamarin.Forms.DependencyService.Get<AzureService>();

            LoginFacebookCommand = new DelegateCommand(async () => await ExecuteLoginFacebookCommand());
            LogOutFacebookCommand = new DelegateCommand(async () => await ExecuteLogOutFacebookCommand());

            Privilegio = "Deslogado";
            
            if (Settings.IsLoggedIn)
            {
                if (Settings.IsAdmin)
                    Privilegio = "Barbeiro";
                else
                    Privilegio = "Cliente";
            }
            
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
                {
                    IsVisibleAdminButtons = true;
                    IsVisibleUserButtons = false;
                    Privilegio = "Barbeiro";
                }
                else
                {
                    IsVisibleAdminButtons = false;
                    IsVisibleUserButtons = true;
                    Privilegio = "Cliente";
                }

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
                Privilegio = "Deslogado";
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
