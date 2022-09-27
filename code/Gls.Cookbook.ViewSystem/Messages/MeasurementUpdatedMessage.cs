using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.ViewSystem.Messages
{
    public class MeasurementUpdatedMessage
    {
        public Measurement Measurement { get; set; }
    }
}
