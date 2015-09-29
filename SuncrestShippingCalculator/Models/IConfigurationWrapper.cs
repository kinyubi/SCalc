using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suncrest.ShippingCalculator.Models
{
    public interface IConfigurationWrapper
    {
        string GetValue(string key);
        bool HasKey(string key);
    }
}
