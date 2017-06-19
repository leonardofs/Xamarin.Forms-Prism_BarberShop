using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        

        public AboutPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Sobre Nós";
            //teste de push jm
        }
    }
}
