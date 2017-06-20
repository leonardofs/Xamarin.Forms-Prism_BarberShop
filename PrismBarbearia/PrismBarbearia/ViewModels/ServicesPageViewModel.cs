using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismBarbearia.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class ServicesPageViewModel : BaseViewModel
    {
        public string Token { get; }
        public string UserId { get; }

        public ServicesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Serviços";            
            Token = Settings.AuthToken;
            UserId = Settings.UserId;
        }
    }
    
}
