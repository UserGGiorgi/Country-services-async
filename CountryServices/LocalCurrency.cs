namespace CountryServices
{
    /// <summary>
    /// Contains information about local currency of the country.
    /// </summary>
    public class LocalCurrency
    {
        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        public string? CountryName { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        public string? CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the currency symbol.
        /// </summary>
        public string? CurrencySymbol { get; set; }
    }
}
