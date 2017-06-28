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

        private bool isVisibleLogInButton;
        public bool IsVisibleLogInButton
        {
            get { return isVisibleLogInButton; }
            set { SetProperty(ref isVisibleLogInButton, value); }
        }

        private bool isVisibleLogOutButton;
        public bool IsVisibleLogOutButton
        {
            get { return isVisibleLogOutButton; }
            set { SetProperty(ref isVisibleLogOutButton, value); }
        }

        private bool isVisibleAdminButtons;
        public bool IsVisibleAdminButtons
        {
            get { return isVisibleAdminButtons; }
            set { SetProperty(ref isVisibleAdminButtons, value); }
        }

        private bool isVisibleUserButtons;
        public bool IsVisibleUserButtons
        {
            get { return isVisibleUserButtons; }
            set { SetProperty(ref isVisibleUserButtons, value); }
        }

        protected INavigationService _navigationService { get; }
        protected IPageDialogService _pageDialogService { get; }        

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            if (!CrossConnectivity.Current.IsConnected)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
            }

            IsVisibleAdminButtons = Settings.IsAdmin;
            IsVisibleUserButtons = !Settings.IsAdmin;
            IsVisibleLogInButton = !Settings.IsLoggedIn;
            IsVisibleLogOutButton = Settings.IsLoggedIn;            
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