using Xamarin.Forms;
using Xamarin.Forms.Maps;
namespace PrismBarbearia.Views
{
    public partial class ContactTabPage : ContentPage
    {
        public ContactTabPage()
        {
            InitializeComponent();

        }

       protected override void OnAppearing()
        {
            base.OnAppearing();

            MyMap.MoveToRegion(
              MapSpan.FromCenterAndRadius(
                 new Position(-20.172100, -44.912956), Distance.FromKilometers(2)));

            MyMap.MapType = MapType.Hybrid;
            var position = new Position(-20.172100, -44.912956); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "A barbearia",
                Address = "R. Paraná, 3001 - Jardim Belvedere, Divinópolis - MG"
                
            };
            MyMap.Pins.Add(pin);

        }


    }
}
