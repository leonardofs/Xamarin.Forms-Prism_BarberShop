using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using PrismBarbearia.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismBarbearia.ViewModels
{
    public class ServicesPageViewModel : BaseViewModel, INavigatedAware
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

        public DelegateCommand AgendarButtonCommand { get; }
        public DelegateCommand CancelarButtonCommand { get; }


        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public ServicesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "LISTA DE SERVIÇOS";
            azureDataService = Xamarin.Forms.DependencyService.Get<AzureDataService>();

            NomeEntry = "";
            TelefoneEntry = "";
            AgendarButtonCommand = new DelegateCommand(async () => await ExecuteAgendarButtonCommand());
            CancelarButtonCommand = new DelegateCommand(async () => await ExecuteCancelarButtonCommand());
        }

        BarberSchedule Schedule = new BarberSchedule();

        public override void OnNavigatedTo(NavigationParameters navigationParams)
        {
            DateTime dateTimeTapped = navigationParams.GetValue<DateTime>("dateTime");

            Schedule.DateTime = dateTimeTapped;
        }

        public async Task ExecuteAgendarButtonCommand()
        {
            Schedule.Service = NomeEntry;
            await azureDataService.AddScheduleOut(Schedule.Service, TelefoneEntry, Schedule.DateTime);
            await _navigationService.GoBackAsync();
        }

        private void canExecuteAgendarButtonChanged()
        {
            if (NomeEntry.Length > 2 && TelefoneEntry.Length > 7)
                CanExecuteAgendarButton = true;
            else
                CanExecuteAgendarButton = false;
        }

        public async Task ExecuteCancelarButtonCommand()
        {
            await _navigationService.GoBackAsync();
        }

    }
}