namespace CountryServices.Tests.Comparators;

/// <summary>
/// Present equality comparer by value of LocalCurrency objects.
/// </summary>
public class CountryInfoComparer : IEqualityComparer<Country>
{
    /// <summary>
    /// Compare two LocalCurrency objects.
    /// </summary>
    /// <param name="x">First parameter.</param>
    /// <param name="y">Second parameter.</param>
    /// <returns>true if two LocalCurrency object equals by value; false otherwise.</returns>
    public bool Equals(Country? x, Country? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Name == y.Name && x.CapitalName == y.CapitalName;
    }

    /// <summary>
    /// Calculate hash code of object.
    /// </summary>
    /// <param name="obj">Source object.</param>
    /// <returns>Returns the hash code of the object.</returns>
    public int GetHashCode(Country obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        return HashCode.Combine(obj.Name, obj.CapitalName);
    }
}
