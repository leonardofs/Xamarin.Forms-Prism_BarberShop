using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using PrismBarbearia.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismBarbearia.ViewModels
{
    public class EditServicesPageViewModel : BaseViewModel
    {
        private string newServiceEntry;
        public string NewServiceEntry
        {
            get { return newServiceEntry; }
            set
            {
                SetProperty(ref newServiceEntry, value);
                CanExecuteAdicionarButtonChanged();
            }
        }

        private string servicePriceEntry;
        public string ServicePriceEntry
        {
            get { return servicePriceEntry; }
            set
            {
                SetProperty(ref servicePriceEntry, value);
                CanExecuteAdicionarButtonChanged();
            }
        }

        private BarberService selectedService;
        public BarberService SelectedService
        {
            get { return selectedService; }
            set
            {
                SetProperty(ref selectedService, value);
                CanExecuteEditarButtonChanged();
            }
        }

        private bool canExecuteAdicionarButton;
        public bool CanExecuteAdicionarButton
        {
            get { return canExecuteAdicionarButton; }
            set { SetProperty(ref canExecuteAdicionarButton, value); }
        }

        private bool canExecuteEditarButton;
        public bool CanExecuteEditarButton
        {
            get { return canExecuteEditarButton; }
            set { SetProperty(ref canExecuteEditarButton, value); }
        }

        public DelegateCommand AdicionarButtonCommand { get; set; }
        public DelegateCommand EditarButtonCommand { get; set; }
        public DelegateCommand DeletarButtonCommand { get; set; }

        private AzureDataService azureDataService;

        public ObservableCollection<BarberService> BarberServicesList { get; set; }


        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public EditServicesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "EDIÇÃO DA LISTA DE SERVIÇOS";
            BarberServicesList = new ObservableCollection<BarberService>();

            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();

            SyncServices();

            AdicionarButtonCommand = new DelegateCommand(async () => await ExecuteAdicionarButtonCommand());
            EditarButtonCommand = new DelegateCommand(async () => await ExecuteEditarButtonCommand());
            DeletarButtonCommand = new DelegateCommand(async () => await ExecuteDeletarButtonCommand());
        }

        private void CanExecuteAdicionarButtonChanged()
        {
            if (NewServiceEntry != null && ServicePriceEntry != null)
                CanExecuteAdicionarButton = true;
            else
                CanExecuteAdicionarButton = false;
        }

        private void CanExecuteEditarButtonChanged()
        {
            if (SelectedService != null)
            {
                CanExecuteEditarButton = true;
                NewServiceEntry = SelectedService.ServiceName;
                ServicePriceEntry = SelectedService.ServicePrice.Substring(3);
            }
            else
            {
                CanExecuteEditarButton = false;
                NewServiceEntry = "";
                ServicePriceEntry = "";
            }
        }

        private async Task ExecuteDeletarButtonCommand()
        {
            IsBusy = true;
            var Repository = new Repository();
            var Items = await Repository.GetServices();
            foreach (var Service in Items)
            {
                if (SelectedService.ServiceName == Service.ServiceName)
                {
                    await azureDataService.RemoveService(Service.Id);
                    SyncServices();
                }
            }
        }

        private async Task ExecuteEditarButtonCommand()
        {
            var Repository = new Repository();
            var Items = await Repository.GetServices();
            foreach (var Service in Items)
            {
                if (SelectedService.ServiceName == Service.ServiceName)
                {
                    await azureDataService.RemoveService(Service.Id);
                    await azureDataService.AddService(NewServiceEntry, ServicePriceEntry);
                    SyncServices();
                }
            }
        }

        private async Task ExecuteAdicionarButtonCommand()
        {
            var Repository = new Repository();
            var Items = await Repository.GetServices();
            foreach (var Service in Items)
            {
                if (NewServiceEntry == Service.ServiceName)
                {
                    await _pageDialogService.DisplayAlertAsync("Serviço já existe", "Não é possível adicionar outro serviço com o mesmo nome", "Ok");
                    return;
                }                    
            }
            await azureDataService.AddService(NewServiceEntry, ServicePriceEntry);
            SyncServices();
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
        }
    }
}
