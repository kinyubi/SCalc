
namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Represents an entry in the table that associates zip codes with shipping zones
    /// </summary>
    public class ShippingZone
    {
        /// <summary>
        /// Gets or sets the zip code. Many-to-one relationship with Zone. Each zipcode in the table
        /// should be unique.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the zone associated with the zipcode. One-to-many relationship with Zip.
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingZone"/> class.
        /// </summary>
        /// <param name="zip">The zipcode.</param>
        /// <param name="zone">The zone to associate with the zipcode.</param>
        public ShippingZone(string zip, string zone)
        {
            Zip = zip;
            Zone = zone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingZone"/> class.
        /// </summary>
        public ShippingZone() : this(null, null) { }
    }
}