using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.ViewSystem.Messages;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class EditMeasurementViewModel : ObservableObject, IViewModel<int>
    {
        private INavigationService navigationService;
        private ISnackBarService snackBarService;
        private IQueryMeasurementService queryMeasurementService;
        private ICommandService<DeleteMeasurementCommand> deleteMeasurementService;

        private int measurementId;

        private MeasurementType measurementType;
        public MeasurementType MeasurementType
        {
            get
            {
                return measurementType;
            }
            set
            {
                SetProperty(ref measurementType, value);
            }
        }

        private MeasurementSystem measurementSystem;
        public MeasurementSystem MeasurementSystem
        {
            get
            {
                return measurementSystem;
            }
            set
            {
                SetProperty(ref measurementSystem, value);
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        private string abbreviation;
        public string Abbreviation
        {
            get
            {
                return abbreviation;
            }
            set
            {
                SetProperty(ref abbreviation, value);
            }
        }

        public IAsyncRelayCommand DeleteMeasurementCommand { get; }

        public EditMeasurementViewModel(INavigationService navigationService, ISnackBarService snackBarService, IQueryMeasurementService queryMeasurementService, ICommandService<DeleteMeasurementCommand> deleteMeasurementService)
        {
            this.navigationService = navigationService;
            this.snackBarService = snackBarService;
            this.queryMeasurementService = queryMeasurementService;
            this.deleteMeasurementService = deleteMeasurementService;

            this.DeleteMeasurementCommand = new AsyncRelayCommand(DeleteMeasurement);
        }

        private async Task DeleteMeasurement()
        {
            Result result = await deleteMeasurementService.ExecuteAsync(new DeleteMeasurementCommand() { Id = measurementId });
            if (result.Success)
            {
                // notify listeners and go back
                WeakReferenceMessenger.Default.Send(new MeasurementDeletedMessage() { MeasurementId = this.measurementId, MeasurementType = this.MeasurementType, MeasurementSystem = this.MeasurementSystem });
                await navigationService.GoBackAsync();
            }
            else
            {
                // toast the user with the failure
                await snackBarService.ShowAsync(result.Message);
            }
        }

        public async Task InitializeAsync(int measurementId)
        {
            Measurement measurement = await queryMeasurementService.GetByIdAsync(measurementId);

            this.measurementId = measurement.Id;
            this.MeasurementType = measurement.MeasurementType;
            this.MeasurementSystem = measurement.MeasurementSystem;
            this.Name = measurement.Name;
            this.Abbreviation = measurement.Abbreviation;
        }
    }
}
