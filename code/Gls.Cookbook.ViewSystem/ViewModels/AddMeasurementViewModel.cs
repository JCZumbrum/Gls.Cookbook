using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AddMeasurementViewModel : ObservableObject, IViewModel<MeasurementType>
    {
        public class ObservableMeasurementSystem : ObservableObject
        {
            public MeasurementSystem MeasurementSystem { get; set; }

            public string Name
            {
                get
                {
                    return MeasurementSystem.ToString();
                }
            }
        }

        private INavigationService navigationService;
        private ISnackBarService snackBarService;
        private ICommandService<CreateMeasurementCommand, Measurement> createMeasurementService;

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

        private ObservableMeasurementSystem measurementSystem;
        public ObservableMeasurementSystem MeasurementSystem
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

        public IAsyncRelayCommand AddMeasurementCommand { get; }

        public ObservableCollection<ObservableMeasurementSystem> MeasurementSystems { get; } = new ObservableCollection<ObservableMeasurementSystem>();

        public AddMeasurementViewModel(INavigationService navigationService, ISnackBarService snackBarService, ICommandService<CreateMeasurementCommand, Measurement> createMeasurementService)
        {
            this.navigationService = navigationService;
            this.snackBarService = snackBarService;
            this.createMeasurementService = createMeasurementService;

            this.AddMeasurementCommand = new AsyncRelayCommand(AddMeasurmentAsync);

            this.MeasurementSystems.Add(new ObservableMeasurementSystem() { MeasurementSystem = Domain.MeasurementSystem.UsCustomary });
            this.MeasurementSystems.Add(new ObservableMeasurementSystem() { MeasurementSystem = Domain.MeasurementSystem.Metric });
        }

        public Task InitializeAsync(MeasurementType args)
        {
            this.MeasurementType = args;

            return Task.CompletedTask;
        }

        private async Task AddMeasurmentAsync()
        {
            Result<Measurement> result = await createMeasurementService.ExecuteAsync(new CreateMeasurementCommand() { MeasurementType = this.MeasurementType, MeasurementSystem = this.MeasurementSystem.MeasurementSystem, Name = this.Name, Abbreviation = this.Abbreviation });
            if (result.Success)
            {
                // notify listeners and go back
                WeakReferenceMessenger.Default.Send(new MeasurementAddedMessage() { Measurement = result.Value });
                await navigationService.GoBackAsync();
            }
            else
            {
                // toast the user with the failure
                await snackBarService.ShowAsync(result.Message);
            }
        }
    }
}
