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
        private string privilegio;
        public string Privilegio
        {
            get { return privilegio; }
            set { SetProperty(ref privilegio, value); }
        }

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

        private bool isVisibleMainPageButton;
        public bool IsVisibleMainPageButton
        {
            get { return isVisibleMainPageButton; }
            set { SetProperty(ref isVisibleMainPageButton, value); }
        }

        protected AzureService azureService;

        public DelegateCommand LoginFacebookCommand { get; private set; }
        public DelegateCommand LogOutFacebookCommand { get; private set; }
        public DelegateCommand SchedulesWeekPageCommand { get; private set; }
        public DelegateCommand MainPageCommand { get; private set; }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public MenuPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            azureService = Xamarin.Forms.DependencyService.Get<AzureService>();

            LoginFacebookCommand = new DelegateCommand(async () => await ExecuteLoginFacebookCommand());
            LogOutFacebookCommand = new DelegateCommand(() => ExecuteLogOutFacebookCommand());
            SchedulesWeekPageCommand = new DelegateCommand(async () => await ExecuteSchedulesWeekPageCommand());
            MainPageCommand = new DelegateCommand(async () => await ExecuteMainPageCommand());


            Privilegio = "Faça o login";
            if (Settings.IsLoggedIn)
            {
                if (Settings.IsAdmin)
                    Privilegio = "Barbeiro";
                else
                    Privilegio = "Cliente";
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
            }

            isVisibleAdminButtons = Settings.IsAdmin;
            isVisibleLogInButton = !Settings.IsLoggedIn;
            isVisibleLogOutButton = Settings.IsLoggedIn;
            isVisibleMainPageButton = false;
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
                    Privilegio = "Barbeiro";
                }
                else
                {
                    IsVisibleAdminButtons = false;
                    Privilegio = "Cliente";
                }

            }
            else //Se desconectado
            {
                await _pageDialogService.DisplayAlertAsync("Sem rede", "não é possível fazer login sem conexão com a internet", "OK");
            }
        }

        private void ExecuteLogOutFacebookCommand()
        {
            if (Settings.IsLoggedIn)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                IsVisibleLogInButton = true;
                IsVisibleLogOutButton = false;
                IsVisibleAdminButtons = false;
                Privilegio = "Faça o login";
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

        private async Task ExecuteSchedulesWeekPageCommand()
        {
            await _navigationService.NavigateAsync("Navigation/SchedulesWeekPage", useModalNavigation:false);
            IsVisibleAdminButtons = !Settings.IsAdmin;
            IsVisibleMainPageButton = true;
        }

        private async Task ExecuteMainPageCommand()
        {
            await _navigationService.NavigateAsync("Navigation/MainPage", useModalNavigation: false);
            IsVisibleAdminButtons = Settings.IsAdmin;
            IsVisibleMainPageButton = false;
        }

    }
}