using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
    
        protected INavigationService _navigationService { get; }

        public MainPageViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }
    }
}
