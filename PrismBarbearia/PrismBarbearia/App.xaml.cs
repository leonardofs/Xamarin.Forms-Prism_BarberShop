using Prism.Unity;
using PrismBarbearia.Views;

namespace PrismBarbearia
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("MenuPage/MyNavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MyNavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MenuPage>();
            Container.RegisterTypeForNavigation<AboutTabPage>();
            Container.RegisterTypeForNavigation<ContactTabPage>();
            Container.RegisterTypeForNavigation<ScheduleTabPage>();
            Container.RegisterTypeForNavigation<ServicesPage>();
            Container.RegisterTypeForNavigation<SchedulesWeekPage>();
            Container.RegisterTypeForNavigation<DaysPage>();
            Container.RegisterTypeForNavigation<HoursPage>();
        }
    }
}
