using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public MeasurementSystem MeasurementSystem { get; set; }
    }
}
