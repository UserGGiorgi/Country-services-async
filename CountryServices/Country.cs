namespace CountryServices;

/// <summary>
/// Contains information about capital of the country.
/// </summary>
public class Country
{
    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the capital name of the country.
    /// </summary>
    public string? CapitalName { get; set; }

    /// <summary>
    /// Gets or sets the area of the country.
    /// </summary>
    public double Area { get; set; }

    /// <summary>
    /// Gets or sets the population of the country.
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets the flag url (png or svg if png does not exist) of the country.
    /// </summary>
    public string? Flag { get; set; }
}
