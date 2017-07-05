﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismBarbearia.Models;
using PrismBarbearia.Services;
using System;
using System.Collections.ObjectModel;

namespace PrismBarbearia.ViewModels
{
    public class DaysPageViewModel : BaseViewModel, INavigatedAware
    {
        public ObservableCollection<BarberDay> Days { get; }
        private NavigationParameters _navigationParams;

        //--------------------------------------------------CONSTRUTOR-------------------------------------------------//
        public DaysPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Selecione um dia:";
            Days = new ObservableCollection<BarberDay>();
            FillDaysCollection();
        }

        private void FillDaysCollection()
        {
            for (int i = 0; i < 15; i++)
            {
                DateTime date = DateTime.Today.AddDays(i);
                BarberDay day = new BarberDay();
                day.Date = date.ToString("dd/MM/yyyy");
                if (date.DayOfWeek == 0)
                    continue;
                else
                    Days.Add(day);
            }
        }

        public override void OnNavigatedTo(NavigationParameters navigationParams)
        {
            BarberService serviceTapped = navigationParams.GetValue<BarberService>("serviceTapped");
            _navigationParams = navigationParams;

            if (navigationParams.GetNavigationMode() == 0)
                _navigationService.GoBackAsync(null, false);
        }

        public async void Navigate(object dayTapped)
        {
            if (dayTapped != null)
            {
                _navigationParams.Add("dayTapped", dayTapped);
                await _navigationService.NavigateAsync("HoursPage", _navigationParams, false);
            }
        }

    }
}