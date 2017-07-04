using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using System;
using System.Collections.ObjectModel;

namespace PrismBarbearia.ViewModels
{
    public class DaysPageViewModel : BaseViewModel, INavigatedAware
    {
        public ObservableCollection<string> Days { get; }
        public BarberService Service { get; set; }
        private string _selecteDay;

        public string SelectedDay
        {
            get { return _selecteDay; }
            set
            {
                SetProperty(ref _selecteDay, value);
                if(SelectedDay != null)
                    NavigationHoursViewModel();
            }
                 
        }

        private void NavigationHoursViewModel()
        {
            _navigationService.NavigateAsync("HoursPage", null, false);
        }
        public DaysPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Selecione um dia:";
            Days = new ObservableCollection<string>();
            FillDaysCollection();
        }

        private void FillDaysCollection()
        {
            for(int i = 0; i<15; i++)
            {
                DateTime date = DateTime.Today.AddDays(i);
                string day = date.ToString("dd/MM/yyyy");
                if (date.DayOfWeek == 0)
                    continue;
                else
                 Days.Add(day);
            }
        }
    }
}
