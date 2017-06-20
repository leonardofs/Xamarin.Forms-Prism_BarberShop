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

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    {
        //servico de alertas
        IPageDialogService _pageDialogService;

        public DelegateCommand CheckConnectionCommand{ get; private set; }

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

        public SchedulesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Agendar";


            //instanciando servico de alertas
            _pageDialogService = pageDialogService;

            CheckConnectionCommand = new DelegateCommand(CheckConnection);

            IsConnected = CrossConnectivity.Current.IsConnected;
            NotConnected = !IsConnected;  

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
       

    }
}

