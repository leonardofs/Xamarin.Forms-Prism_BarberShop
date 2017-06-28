using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismBarbearia.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {        
        public AboutPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "A Barbearia";
        }
    }
}
