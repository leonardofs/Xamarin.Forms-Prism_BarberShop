using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Plugin.Messaging;
using System.Threading.Tasks;

namespace PrismBarbearia.ViewModels
{
    public class ContactTabPageViewModel : BaseViewModel
    {

       public DelegateCommand CallBarberCommand { get; set; }

        public ContactTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "ONDE ESTAMOS";
           CallBarberCommand= new DelegateCommand(CallBarber);
        }


        private void CallBarber() {
             CrossMessaging.Current.PhoneDialer.MakePhoneCall("3732222222", "Barbearia");
            
        }

    }
}