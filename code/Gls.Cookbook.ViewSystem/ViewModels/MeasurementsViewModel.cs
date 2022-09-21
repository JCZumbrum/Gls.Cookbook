﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gls.Cookbook.Domain;
using Gls.Cookbook.ViewSystem.Args;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public partial class MeasurementsViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        private INavigationService navigationService;

        public IAsyncRelayCommand VolumeMeasurementsCommand { get; }
        public IAsyncRelayCommand WeightMeasurementsCommand { get; }

        public MeasurementsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.VolumeMeasurementsCommand = new AsyncRelayCommand(ViewVolumeMeasurements);
            this.WeightMeasurementsCommand = new AsyncRelayCommand(ViewWeightMeasurements);
        }

        private async Task ViewVolumeMeasurements()
        {
            await navigationService.GoToAsync<MeasurementListViewModel, MeasurementType>(MeasurementType.Volume);
        }

        private async Task ViewWeightMeasurements()
        {
            await navigationService.GoToAsync<MeasurementListViewModel, MeasurementType>(MeasurementType.Weight);
        }

        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }
    }
}
