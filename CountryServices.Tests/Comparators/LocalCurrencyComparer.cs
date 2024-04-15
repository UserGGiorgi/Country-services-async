namespace CountryServices.Tests.Comparators;

/// <summary>
/// Present equality comparer by value of LocalCurrency objects.
/// </summary>
public class LocalCurrencyComparer : IEqualityComparer<LocalCurrency>
{
    /// <summary>
    /// Compare two LocalCurrency objects.
    /// </summary>
    /// <param name="x">First parameter.</param>
    /// <param name="y">Second parameter.</param>
    /// <returns>true if two LocalCurrency object equals by value; false otherwise.</returns>
    public bool Equals(LocalCurrency? x, LocalCurrency? y)
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

        return x.CountryName == y.CountryName &&
            x.CurrencyCode == y.CurrencyCode &&
            x.CurrencySymbol == y.CurrencySymbol;
    }

    /// <summary>
    /// Calculate hash code of object.
    /// </summary>
    /// <param name="obj">Source object.</param>
    /// <returns>Returns the hash code of the object.</returns>
    public int GetHashCode(LocalCurrency obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        return HashCode.Combine(obj.CountryName, obj.CurrencyCode, obj.CurrencySymbol);
    }
}
