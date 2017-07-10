using Prism.Unity;
using PrismBarbearia.Views;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

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
            MobileCenter.Start("uwp=a5206e2c-dd25-414f-b9f3-eebaa3bce4bb;" +
                   "android=d0d959cd-a188-411c-912e-b7120e0e4c42;" +
                   "ios=a7e7daaf-e142-4ae8-9797-bad797ea2e8d;",
                   typeof(Analytics), typeof(Crashes));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MyNavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MenuPage>();
            Container.RegisterTypeForNavigation<AboutTabPage>();
            Container.RegisterTypeForNavigation<ContactTabPage>();
            Container.RegisterTypeForNavigation<ScheduleTabPage>();
            Container.RegisterTypeForNavigation<NewEventPage>();
            Container.RegisterTypeForNavigation<SchedulesWeekPage>();
            Container.RegisterTypeForNavigation<DaysPage>();
            Container.RegisterTypeForNavigation<HoursPage>();
            Container.RegisterTypeForNavigation<EventStatusPage>();
            Container.RegisterTypeForNavigation<EditServicesPage>();
            Container.RegisterTypeForNavigation<HoursAdminPage>();
        }
    }
}
