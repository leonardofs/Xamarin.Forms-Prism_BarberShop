using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using PrismBarbearia.Models;
using PrismBarbearia.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureDataService))]

namespace PrismBarbearia.Services
{
    public class AzureDataService
    {

        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<BarberService> serviceTable;

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            //var appUrl = "http://appxamarindemo.azurewebsites.net";
            var appUrl = "http://barbearia8ball.azurewebsites.net";

            Client = new MobileServiceClient(appUrl);
            
            //InitializeDatabase for path
            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<BarberService>();

            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            serviceTable = Client.GetSyncTable<BarberService>();
        }

        public async Task SyncService()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await serviceTable.PullAsync("agendamentosFeitos", serviceTable.CreateQuery());
                await Client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }

        }

        public async Task<IEnumerable<BarberService>> GetServices()
        {
            await Initialize();
            await SyncService();

            return await serviceTable.ToEnumerableAsync(); ;
        }

        public async Task<BarberService> AddService(string name, string price)//, string id, string detail, string image)
        {
            await Initialize();

            var service = new BarberService
            {
                //Id = id,
                ServiceName = name,
                ServicePrice = price,
                //Detail = detail,
                //Image = image
            };

            await serviceTable.InsertAsync(service);
            await SyncService();
            return service;
        }            

    }
}
