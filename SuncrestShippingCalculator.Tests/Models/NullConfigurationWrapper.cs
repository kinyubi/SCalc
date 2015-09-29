using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suncrest.ShippingCalculator.Models;

namespace Suncrest.ShippingCalculator.Tests.Models
{
    public class NullConfigurationWrapper : IConfigurationWrapper
    {
        public string GetValue(string key)
        {
            return null;
        }

        public bool HasKey(string key)
        {
            return false;
        }
    }
}
