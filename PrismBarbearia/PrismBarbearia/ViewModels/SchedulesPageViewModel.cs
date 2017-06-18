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

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    {
        //servico de alertas
        IPageDialogService _pageDialogService;

        public DelegateCommand CheckConnectionCommand { get; set; }
        public SchedulesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Agendar";
            //instanciando servico de alertas
            _pageDialogService = pageDialogService;

            CheckConnectionCommand = new DelegateCommand(CheckConnection);

           // CheckConnectionCommand.Execute();
        }


        private async void CheckConnection()
        {
           // Se desconectado
            if (!CrossConnectivity.Current.IsConnected)
            {
                await _pageDialogService.DisplayAlertAsync("Sem rede","não é possivél realizar agendamentos sem conexão com a internet","OK");
                
                //TODO  texto na tela informando que nao ha conexao, destivar botao de login
            }
            else //Se houver conexão
            {
                await _pageDialogService.DisplayAlertAsync("Com rede", "teste de rede ok", "OK");
                //TODO ativar botao de login e escrever texto pedindo login com rede social
            }
        }


    }
}

