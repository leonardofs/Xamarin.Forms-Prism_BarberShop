using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class ServicesPageViewModel : BaseViewModel
    {
        public ServicesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            //TODO lista de servicos
        }
    }    
}
