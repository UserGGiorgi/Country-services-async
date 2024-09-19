using System.Text.Json;

namespace CountryServices;

/// <summary>
/// Provides information about country local currency from RESTful API
/// <see><cref>https://restcountries.com/#api-endpoints-v2</cref></see>.
/// </summary>
public class CountryService : ICountryService
{
    private const string ServiceUrl = "https://restcountries.com/v2";

    private readonly Dictionary<string, WeakReference<LocalCurrency>> currencyCountries = [];

    /// <summary>
    /// Gets information about currency by country code synchronously.
    /// </summary>
    /// <param name="alpha2Or3Code">ISO 3166-1 2-letter or 3-letter country code.</param>
    /// <see><cref>https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes</cref></see>
    /// <returns>Information about country currency as <see cref="LocalCurrency"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if countryCode is null, empty, whitespace or invalid country code.</exception>
    public LocalCurrency GetLocalCurrencyByAlpha2Or3Code(string? alpha2Or3Code)
    {
        if (string.IsNullOrWhiteSpace(alpha2Or3Code))
        {
            throw new ArgumentException("Country code cannot be null, empty, or whitespace.", nameof(alpha2Or3Code));
        }

        // Check cache
        if (this.currencyCountries.TryGetValue(alpha2Or3Code, out var weakCurrency) && weakCurrency.TryGetTarget(out var cachedCurrency))
        {
            return cachedCurrency;
        }

        try
        {
            using var httpClient = new HttpClient();
            var uri = new Uri($"{ServiceUrl}/alpha/{alpha2Or3Code}");
            var response = httpClient.GetAsync(uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                // Handle 404 Not Found or any other non-success status code.
                throw new ArgumentException($"Invalid country code: {alpha2Or3Code}", nameof(alpha2Or3Code));
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var countryInfo = JsonSerializer.Deserialize<LocalCurrencyInfo>(jsonResponse);

            if (countryInfo == null || countryInfo.Currencies == null || countryInfo.Currencies.Length == 0)
            {
                throw new InvalidOperationException($"No currency information found for country code: {alpha2Or3Code}");
            }

            var currencyData = countryInfo.Currencies[0];
            var localCurrency = new LocalCurrency
            {
                CountryName = countryInfo.CountryName ?? "Unknown",
                CurrencyCode = currencyData.Code ?? "Unknown",
                CurrencySymbol = currencyData.Symbol ?? "Unknown",
            };

            this.currencyCountries[alpha2Or3Code] = new WeakReference<LocalCurrency>(localCurrency);
            return localCurrency;
        }
        catch (HttpRequestException ex)
        {
            throw new ArgumentException($"Error retrieving currency information for country code: {alpha2Or3Code}.", ex);
        }
    }

    /// <summary>
    /// Gets information about currency by country code asynchronously.
    /// </summary>
    /// <param name="alpha2Or3Code">ISO 3166-1 2-letter or 3-letter country code.</param>
    /// <see><cref>https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes</cref></see>.
    /// <param name="token">Token for cancellation asynchronous operation.</param>
    /// <returns>Information about country currency as <see cref="LocalCurrency"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if countryCode is null, empty, whitespace or invalid country code.</exception>
    public async Task<LocalCurrency> GetLocalCurrencyByAlpha2Or3CodeAsync(string? alpha2Or3Code, CancellationToken token)
    {
        ValidateParameters(alpha2Or3Code);
        ArgumentNullException.ThrowIfNull(alpha2Or3Code);
        if (this.currencyCountries.TryGetValue(alpha2Or3Code, out var weakCurrency) && weakCurrency.TryGetTarget(out var cachedCurrency))
        {
            return cachedCurrency;
        }

        return await this.FetchLocalCurrencyAsync(alpha2Or3Code, token);
    }

    /// <summary>
    /// Gets information about the country by the country capital synchronously.
    /// </summary>
    /// <param name="capital">Capital name.</param>
    /// <returns>Information about the country as <see cref="Country"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if the capital name is null, empty, whitespace or nonexistent.</exception>
    public Country GetCountryInfoByCapital(string? capital)
    {
        if (string.IsNullOrWhiteSpace(capital))
        {
            throw new ArgumentException("Capital name cannot be null, empty, or whitespace.", nameof(capital));
        }

        // Directly fetch from the API
        return FetchCountryByCapitalFromApi(capital);
    }

    /// <summary>
    /// Gets information about the currency by the country capital asynchronously.
    /// </summary>
    /// <param name="capital">Capital name.</param>
    /// <param name="token">Token for cancellation asynchronous operation.</param>
    /// <returns>Information about the country as <see cref="Country"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if the capital name is null, empty, whitespace or nonexistent.</exception>
    public async Task<Country> GetCountryInfoByCapitalAsync(string? capital, CancellationToken token)
    {
        ValidateParameters(capital);
        ArgumentNullException.ThrowIfNull(capital);

        return await FetchCountryByCapitalFromApiAsync(capital, token);
    }

    private static async Task<Country> FetchCountryByCapitalFromApiAsync(string capital, CancellationToken token)
    {
        try
        {
            using var httpClient = new HttpClient();
            var uri = new Uri($"{ServiceUrl}/capital/{capital}");
            var response = await httpClient.GetAsync(uri, token);

            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Invalid capital name: {capital}", nameof(capital));
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(token);
            var countryInfo = JsonSerializer.Deserialize<List<CountryInfo>>(jsonResponse);

            if (countryInfo == null || countryInfo.Count == 0)
            {
                throw new ArgumentException($"No country found with capital: {capital}", nameof(capital));
            }

            var info = countryInfo.First();
            return new Country
            {
                Name = info.Name,
                CapitalName = info.CapitalName,
                Area = info.Area,
                Population = info.Population,
                Flag = info.Flag,
            };
        }
        catch (HttpRequestException ex)
        {
            throw new ArgumentException($"Error retrieving country information for capital: {capital}.", ex);
        }
    }

    private static Country FetchCountryByCapitalFromApi(string capital)
    {
        try
        {
            using var httpClient = new HttpClient();
            var uri = new Uri($"{ServiceUrl}/capital/{capital}");
            var response = httpClient.GetAsync(uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Invalid capital name: {capital}", nameof(capital));
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var countryInfo = JsonSerializer.Deserialize<List<CountryInfo>>(jsonResponse);

            if (countryInfo == null || countryInfo.Count == 0)
            {
                throw new ArgumentException($"No country found with capital: {capital}", nameof(capital));
            }

            var info = countryInfo.First();
            return new Country
            {
                Name = info.Name,
                CapitalName = info.CapitalName,
                Area = info.Area,
                Population = info.Population,
                Flag = info.Flag,
            };
        }
        catch (HttpRequestException ex)
        {
            throw new ArgumentException($"Error retrieving country information for capital: {capital}.", ex);
        }
    }

    private static void ValidateParameters(string? capital)
    {
        if (string.IsNullOrWhiteSpace(capital))
        {
            throw new ArgumentException("Capital name cannot be null, empty, or whitespace.", nameof(capital));
        }
    }

    private async Task<LocalCurrency> FetchLocalCurrencyAsync(string alpha2Or3Code, CancellationToken token)
    {
        try
        {
            using var httpClient = new HttpClient();
            var uri = new Uri($"{ServiceUrl}/alpha/{alpha2Or3Code}");
            var response = await httpClient.GetAsync(uri, token);

            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Invalid country code: {alpha2Or3Code}", nameof(alpha2Or3Code));
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(token);
            var countryInfo = JsonSerializer.Deserialize<LocalCurrencyInfo>(jsonResponse);

            if (countryInfo == null || countryInfo.Currencies == null || countryInfo.Currencies.Length == 0)
            {
                throw new InvalidOperationException($"No currency information found for country code: {alpha2Or3Code}");
            }

            var currencyData = countryInfo.Currencies[0];
            var localCurrency = new LocalCurrency
            {
                CountryName = countryInfo.CountryName ?? "Unknown",
                CurrencyCode = currencyData.Code ?? "Unknown",
                CurrencySymbol = currencyData.Symbol ?? "Unknown",
            };

            // Cache the result
            this.currencyCountries[alpha2Or3Code] = new WeakReference<LocalCurrency>(localCurrency);
            return localCurrency;
        }
        catch (HttpRequestException ex)
        {
            throw new ArgumentException($"Error retrieving currency information for country code: {alpha2Or3Code}.", ex);
        }
    }
}
