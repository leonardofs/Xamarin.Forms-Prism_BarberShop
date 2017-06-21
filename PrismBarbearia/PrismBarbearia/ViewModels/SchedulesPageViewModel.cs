using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using Prism.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using PrismBarbearia.Services;
using PrismBarbearia.Helpers;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    {
        //servico de alertas
        IPageDialogService _pageDialogService;
        //servico do azure
        AzureService azureService;
        public DelegateCommand CheckConnectionCommand{ get; private set; }
        public DelegateCommand LoginFacebookCommand { get; private set; }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set { SetProperty(ref isConnected, value); }
        }

        private bool notConnected;
        public bool NotConnected
        {
            get { return notConnected; }
            set { SetProperty(ref notConnected, value); }
        }
        //////////////////                                                                       CONSTRUTOR ///////////////
        public SchedulesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Agendar";


            //instanciando servico de alertas
            _pageDialogService = pageDialogService;

            CheckConnectionCommand = new DelegateCommand(CheckConnection);
            IsConnected = CrossConnectivity.Current.IsConnected;
            NotConnected = !IsConnected;

            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;
            azureService = Xamarin.Forms.DependencyService.Get<AzureService>();
            LoginFacebookCommand = new DelegateCommand(async () => await ExecuteLoginFacebookCommand());
        }

        private async void CheckConnection()
        {
           // Se desconectado
            if (!CrossConnectivity.Current.IsConnected)
            {
              //  Debug.Writeline("sem conexao");
                 await _pageDialogService.DisplayAlertAsync("Sem rede","não é possivél realizar agendamentos sem conexão com a internet","OK");
                
                //TODO  texto na tela informando que nao ha conexao, destivar botao de login
            }
            else //Se houver conexão
            {
                
                
                //TODO ativar botao de login e escrever texto pedindo login com rede social
            }
        }

        private async Task ExecuteLoginFacebookCommand()
        {
            if (IsBusy || !(await LoginAsync()))
                return;
            
            else
            {
               await _navigationService.NavigateAsync("ServicesPage");
            }
            IsBusy = false;

        }

        public Task<bool> LoginAsync()
        {
            IsBusy = true;

            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return azureService.LoginAsync();
        }
    }
}

