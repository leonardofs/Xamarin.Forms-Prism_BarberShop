using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class ContactPageViewModel : BaseViewModel
    {    
        public ContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Onde Estamos";
         
        }
    }
}
