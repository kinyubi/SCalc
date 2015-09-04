
namespace Suncrest.ShippingCalculator.Models
{
    public class ShippingZone
    {
        public string Zip { get; set; }
        public string Zone { get; set; }

        public ShippingZone(string zip, string zone)
        {
            Zip = zip;
            Zone = zone;
        }

        public ShippingZone() : this(null, null) { }
    }
}