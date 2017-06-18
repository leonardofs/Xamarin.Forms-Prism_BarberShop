using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class ContactPageViewModel : BaseViewModel
    {
        

        public ContactPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Onde Estamos";
         
        }
    }
}
