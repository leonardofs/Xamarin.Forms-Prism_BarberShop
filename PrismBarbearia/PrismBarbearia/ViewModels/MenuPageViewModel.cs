using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class MenuPageViewModel : BindableBase 
    {
        protected INavigationService _navigationService { get; }

        public DelegateCommand LoginFacebookCommand { get; private set; }

        public MenuPageViewModel(INavigationService navigationService)
        {            
            _navigationService = navigationService;
        }
    }
}
