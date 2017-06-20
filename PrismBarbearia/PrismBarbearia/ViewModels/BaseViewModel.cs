/*using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismBarbearia.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService _navigationService { get; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

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
}*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismBarbearia.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected INavigationService _navigationService { get; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

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

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T _descricao, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(_descricao, value))
            {
                return false;
            }

            _descricao = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var viewModelType = typeof(TViewModel);

            var viewModelTypeName = viewModelType.Name;
            var viewModelWordLength = "ViewModel".Length;
            var viewTypeName = $"SocialLoginMaratona.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page";
            var viewType = Type.GetType(viewTypeName);

            var page = Activator.CreateInstance(viewType) as Page;

            var viewModel = Activator.CreateInstance(viewModelType, args);
            if (page != null)
            {
                page.BindingContext = viewModel;
            }

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public virtual Task LoadAsync()
        {
            return Task.FromResult(0);
        }
    }
}