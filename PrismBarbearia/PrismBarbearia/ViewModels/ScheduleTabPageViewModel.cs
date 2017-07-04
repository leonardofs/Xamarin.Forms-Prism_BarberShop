using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System.Collections.ObjectModel;
using PrismBarbearia.Services;
using System;
using Xamarin.Forms;

namespace PrismBarbearia.ViewModels
{
    public class ScheduleTabPageViewModel : BaseViewModel, INavigationAware
    {
        public ObservableCollection<BarberService> BarberServicesList { get; }
        protected AzureDataService azureDataService;
        private BarberService selectedService;

        public BarberService SelectedService
        {
            get { return selectedService; }
            set
            {
                SetProperty(ref selectedService, value);
                if (SelectedService != null)
                {
                    ExecuteServiceSelected(SelectedService);
                }
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            SelectedService = (BarberService)parameters["Service"];
        }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ScheduleTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();
            Title = "AGENDAR";
            BarberServicesList = new ObservableCollection<BarberService>();
            SyncServices();

        }

        private async void ExecuteServiceSelected(object obj)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("service", new BarberService());
            await _navigationService.NavigateAsync("DaysPage",navigationParams,false);
        }

        async void SyncServices()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                BarberServicesList.Clear();
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetServices();
                    foreach (var Service in Items)
                    {
                        BarberServicesList.Add(Service);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }
                if (Error != null)
                {
                    await _pageDialogService.DisplayAlertAsync("Erro", Error.Message, "OK");
                }
            }
            return;
        }

        public async void Navigate(object serviceTapped)
        {
            if (serviceTapped != null)
            {
                
                BarberService service = serviceTapped as BarberService;
                //await azureDataService.AddService("id52", service.Name, service.Price);
                // await _navigationService.NavigateAsync("DaysPage", serviceTapped as NavigationParameters, false);
            }
        }

    }
}