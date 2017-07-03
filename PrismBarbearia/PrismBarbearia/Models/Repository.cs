using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismBarbearia.Models
{
    class Repository
    {
        public async Task<List<BarberService>> GetServices()
        {
            List<BarberService> Services;
            var URLwebAPI = "http://appxamarindemo.azurewebsites.net/tables/Servicos?zumo-api-version=2.0.0";
            using (var Client = new System.Net.Http.HttpClient())
            {
                var JSON = await Client.GetStringAsync(URLwebAPI);
                Services = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BarberService>>(JSON);
            }
            return Services;
        }
    }
}