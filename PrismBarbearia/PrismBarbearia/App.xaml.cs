using Prism.Unity;
using PrismBarbearia.Views;
using Xamarin.Forms;

namespace PrismBarbearia
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            
            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterTypeForNavigation<MenuPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<AboutPage>();
            Container.RegisterTypeForNavigation<ContactPage>();
            Container.RegisterTypeForNavigation<SchedulesPage>();
        }
    }
}
