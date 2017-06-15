using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class SchedulesPageViewModel : BaseViewModel
    { 
        public SchedulesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "Agenda";
        }
    }
}
