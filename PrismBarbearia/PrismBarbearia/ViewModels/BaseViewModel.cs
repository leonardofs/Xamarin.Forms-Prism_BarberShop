using System;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Commands;
using System.Threading.Tasks;
using Plugin.Connectivity;
using PrismBarbearia.Helpers;
using Prism.Services;
using PrismBarbearia.Services;

namespace PrismBarbearia.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy))); }
        }

        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        protected INavigationService _navigationService { get; }
        protected IPageDialogService _pageDialogService { get; }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

    }
}