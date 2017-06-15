using Xamarin.Forms;

namespace PrismBarbearia.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
            this.Master = new MasterPage();
            this.Detail = new NavigationPage(new MainPage());
            
        }
    }
}