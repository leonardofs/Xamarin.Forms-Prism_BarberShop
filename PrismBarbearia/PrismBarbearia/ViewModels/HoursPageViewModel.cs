using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PrismBarbearia.Models;

namespace PrismBarbearia.ViewModels
{
    public class HoursPageViewModel : BaseViewModel
    {
        public ObservableCollection<BarberHour> Hours {get;}
        public ObservableCollection<BarberSchedule> Schedules { get; }
        public HoursPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Hours = new ObservableCollection<BarberHour>();
            Title = "Horários";
            syncAvaliableHours();
        }

        private void syncAvaliableHours()
        {
            
        }
    }
}
