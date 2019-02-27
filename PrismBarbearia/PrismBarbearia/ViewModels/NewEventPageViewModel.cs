using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using PrismBarbearia.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismBarbearia.ViewModels
{
    public class NewEventPageViewModel : BaseViewModel, INavigatedAware
    {
        private AzureDataService azureDataService;


        private string nomeEntry;
        public string NomeEntry
        {
            get { return nomeEntry; }
            set
            {
                SetProperty(ref nomeEntry, value);
                canExecuteAgendarButtonChanged();
            }
        }

        private string telefoneEntry;
        public string TelefoneEntry
        {
            get { return telefoneEntry; }
            set
            {
                SetProperty(ref telefoneEntry, value);
                canExecuteAgendarButtonChanged();
            }
        }

        private bool canExecuteAgendarButton;
        public bool CanExecuteAgendarButton
        {
            get { return canExecuteAgendarButton; }
            set { SetProperty(ref canExecuteAgendarButton, value); }
        }

        private BarberService selectedService;
        public BarberService SelectedService
        {
            get { return selectedService; }
            set
            {
                SetProperty(ref selectedService, value);
                canExecuteAgendarButtonChanged();
            }
        }

        public DelegateCommand AgendarButtonCommand { get; }
        public DelegateCommand CancelarButtonCommand { get; }
        public ObservableCollection<BarberService> BarberServicesList { get; }

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public NewEventPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "NOVO AGENDAMENTO";
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();

            NomeEntry = "";
            TelefoneEntry = "";
            AgendarButtonCommand = new DelegateCommand(async () => await ExecuteAgendarButtonCommand());
            CancelarButtonCommand = new DelegateCommand(async () => await ExecuteCancelarButtonCommand());

            BarberServicesList = new ObservableCollection<BarberService>();
            SyncServices();
        }

        BarberSchedule Schedule = new BarberSchedule();

        public override void OnNavigatedTo(NavigationParameters navigationParams)
        {
            DateTime dateTimeTapped = navigationParams.GetValue<DateTime>("dateTime");

            Schedule.DateTime = dateTimeTapped;
        }

        public async Task ExecuteAgendarButtonCommand()
        {
            await azureDataService.AddSchedule(SelectedService.ServiceName, NomeEntry, TelefoneEntry, "email não informado", "aniversário não informado", Schedule.DateTime);

            await _navigationService.GoBackAsync();
        }

        private void canExecuteAgendarButtonChanged()
        {
            if (NomeEntry.Length > 1 && TelefoneEntry.Length > 7 && SelectedService != null)
                CanExecuteAgendarButton = true;
            else
                CanExecuteAgendarButton = false;
        }

        public async Task ExecuteCancelarButtonCommand()
        {
            await _navigationService.GoBackAsync();
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

    }
}