using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        public MasterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "Menu";
        }
    }
}
