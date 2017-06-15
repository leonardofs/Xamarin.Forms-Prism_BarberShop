using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class MasterPageViewModel : BindableBase
    {
        protected INavigationService _navigationService { get; }

        public MasterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
