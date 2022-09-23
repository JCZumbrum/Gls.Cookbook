using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Queries;

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

        public ObservableCollection<ObservableMeasurementSystem> MeasurementSystems { get; } = new ObservableCollection<ObservableMeasurementSystem>();

        public AddMeasurementViewModel(INavigationService navigationService, ICommandService<CreateMeasurementCommand> createMeasurementService)
        {
            this.navigationService = navigationService;
            this.createMeasurementService = createMeasurementService;

            this.MeasurementSystems.Add(new ObservableMeasurementSystem() { MeasurementSystem = Domain.MeasurementSystem.UsCustomary });
            this.MeasurementSystems.Add(new ObservableMeasurementSystem() { MeasurementSystem = Domain.MeasurementSystem.Metric });
        }

        public Task InitializeAsync(MeasurementType args)
        {
            this.MeasurementType = args;

            return Task.CompletedTask;
        }
    }
}
