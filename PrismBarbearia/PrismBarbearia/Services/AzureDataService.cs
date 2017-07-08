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
        IMobileServiceSyncTable<BarberSchedule> scheduleTable;

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
            store.DefineTable<BarberSchedule>();

            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            scheduleTable = Client.GetSyncTable<BarberSchedule>();
            //scheduleTable = Client.GetSyncTable<BarberSchedule>();
        }

        public async Task SyncSchedule()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await scheduleTable.PullAsync("agendamentosFeitos", scheduleTable.CreateQuery());
                await Client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync schedules, that is alright as we have offline capabilities: " + ex);
            }

        }

        public async Task<IEnumerable<BarberSchedule>> GetSchedule()
        {
            await Initialize();
            await SyncSchedule();

            return await scheduleTable.ToEnumerableAsync();
        }
      
        public async Task<BarberSchedule> AddSchedule(string service, DateTime dateTime)
        {
            await Initialize();
            var schedule = new BarberSchedule
            {
                Service = service,
                DateTime = dateTime
            };
            await scheduleTable.InsertAsync(schedule);
            await SyncSchedule();
            return schedule;
        }

        public async Task<BarberSchedule> AddScheduleOut(string service, string phoneNumber, DateTime dateTime)
        {
            await Initialize();
            var schedule = new BarberSchedule
            {
                Service = service,
                PhoneNumber = phoneNumber,
                DateTime = dateTime
            };
            await scheduleTable.InsertAsync(schedule);
            await SyncSchedule();
            return schedule;
        }

    }
}