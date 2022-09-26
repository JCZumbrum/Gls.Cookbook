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
        private IToastService toastService;
        private ICommandService<CreateMeasurementCommand> createMeasurementService;

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

        public AddMeasurementViewModel(INavigationService navigationService, IToastService toastService, ICommandService<CreateMeasurementCommand> createMeasurementService)
        {
            this.navigationService = navigationService;
            this.toastService = toastService;
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
            Result result = await createMeasurementService.ExecuteAsync(new CreateMeasurementCommand() { MeasurementType = this.MeasurementType, MeasurementSystem = this.MeasurementSystem.MeasurementSystem, Name = this.Name, Abbreviation = this.Abbreviation });
            if (result.Success)
            {
                // notify listeners and go back
                WeakReferenceMessenger.Default.Send(new MeasurementAddedMessage());
                await navigationService.GoBackAsync();
            }
            else
            {
                // toast the user with the failure
                toastService.Make(result.Message);
            }
        }
    }
}
