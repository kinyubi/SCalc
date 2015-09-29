using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Suncrest.ShippingCalculator.Models
{

    public class DefaultConfigurationWrapper : IConfigurationWrapper
    {
        /// <summary>
        /// Part of the singleton pattern implementation
        /// </summary>
        private static readonly Lazy<DefaultConfigurationWrapper> lazy = new Lazy<DefaultConfigurationWrapper>(() => new DefaultConfigurationWrapper());

        /// <summary>
        /// Gets the ShippingCostsServiceClient instance.
        /// </summary>
        public static DefaultConfigurationWrapper Instance { get { return lazy.Value; } }

        /// <summary>
        /// Gets the value of the specified appsetting key. Null if not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Value of the specified key</returns>
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Determines whether the specified key exists in appsettings of the config file.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if key exists, otherwise false</returns>
        public bool HasKey(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Select((string x) => x.ToUpperInvariant()).Contains(key.ToUpperInvariant());
        }
    }


}
