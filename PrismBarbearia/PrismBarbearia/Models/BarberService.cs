namespace PrismBarbearia.Models
{
    public class BarberService
    {
        public string Id { get; set; }
        public string Name { get; set; }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = "R$ " + value; }
        }


        public string Image { get; set; }
    }
}