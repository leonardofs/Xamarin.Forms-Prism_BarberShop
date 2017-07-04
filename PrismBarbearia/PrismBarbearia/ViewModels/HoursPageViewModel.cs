using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PrismBarbearia.Models;
using System.Threading.Tasks;

namespace PrismBarbearia.ViewModels
{
    public class HoursPageViewModel : BaseViewModel
    {
        public ObservableCollection<BarberHour> Hours { get; }
        public ObservableCollection<BarberSchedule> Schedules { get; }
        public ObservableCollection<BarberSchedule> Temp { get; set; }
        

        public HoursPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Hours = new ObservableCollection<BarberHour>();
            Schedules = new ObservableCollection<BarberSchedule>();
            Temp = new ObservableCollection<BarberSchedule>();
            Title = "Horários";
            SyncAvaliableHours();
        }

        async void SyncAvaliableHours()
        {
            await SyncSchedules();
            var schedulesAz = Schedules;
            foreach (var item in schedulesAz)
            {
                var listItem = item as BarberSchedule;
                if (listItem.Day == "03/07/2017")
                {
                    Temp.Add(listItem);
                }
            }

        }

        
         async Task SyncSchedules()
        {
            if (!IsBusy)
            {
                Exception Error = null;

                Schedules.Clear();
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetSchedule("03/07/2017");
                    foreach (var Service in Items)
                    {
                        Schedules.Add(Service);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }
                if (Error != null)
                {
                    await _pageDialogService.DisplayAlertAsync("Erro", Error.Message, "OK");
                }
                return;
            }
        }
    }
}
