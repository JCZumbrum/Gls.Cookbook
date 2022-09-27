using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;

namespace Gls.Cookbook.ViewSystem.Messages
{
    public class MeasurementDeletedMessage
    {
        public int MeasurementId { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public MeasurementSystem MeasurementSystem { get; set; }
    }
}
