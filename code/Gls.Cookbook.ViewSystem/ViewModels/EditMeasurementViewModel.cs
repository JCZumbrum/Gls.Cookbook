﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class EditMeasurementViewModel : ObservableObject, IViewModel<int>
    {
        private IQueryMeasurementService queryMeasurementService;

        private int measurementId;

        private string measurementType;
        public string MeasurementType
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

        private string measurementSystem;
        public string MeasurementSystem
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

        public EditMeasurementViewModel(IQueryMeasurementService queryMeasurementService)
        {
            this.queryMeasurementService = queryMeasurementService;
        }

        public async Task InitializeAsync(int measurementId)
        {
            Measurement measurement = await queryMeasurementService.GetByIdAsync(measurementId);

            this.measurementId = measurement.Id;
            this.MeasurementType = measurement.MeasurementType.ToString();
            this.MeasurementSystem = measurement.MeasurementSystem.ToString();
            this.Name = measurement.Name;
            this.Abbreviation = measurement.Abbreviation;
        }
    }
}