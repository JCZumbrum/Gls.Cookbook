﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Commands.Measurements
{
    public class UpdateMeasurementCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public MeasurementSystem MeasurementSystem { get; set; }
    }
}