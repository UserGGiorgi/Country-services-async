using System;
using System.Threading;
using System.Threading.Tasks;

namespace CountryServices
{
    /// <summary>
    /// Provides various information about the country.
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Gets information about the currency by the country code synchronously.
        /// </summary>
        /// <param name="alpha2Or3Code">ISO 3166-1 2-letter or 3-letter country code.</param>
        /// <see>
        ///     <cref>https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes</cref>
        /// </see>
        /// <returns>Information about the country currency as <see cref="LocalCurrency"/>>.</returns>
        /// <exception cref="ArgumentException">Throw if alpha2Or3Code is null, empty, whitespace or invalid country code.</exception>
        LocalCurrency GetLocalCurrencyByAlpha2Or3Code(string? alpha2Or3Code);

        /// <summary>
        /// Gets information about the currency by the country code asynchronously.
        /// </summary>
        /// <param name="alpha2Or3Code">ISO 3166-1 2-letter or 3-letter country code.</param>
        /// <see>
        ///     <cref>https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes</cref>
        /// </see>
        /// <param name="token">Token for the cancellation of asynchronous operation.</param>
        /// <returns>Information about the country currency as <see cref="LocalCurrency"/>>.</returns>
        /// <exception cref="ArgumentException">Throw if alpha2Or3Code is null, empty, whitespace or invalid country code.</exception>
        Task<LocalCurrency> GetLocalCurrencyByAlpha2Or3CodeAsync(string? alpha2Or3Code, CancellationToken token);

        /// <summary>
        /// Gets information about the country by the country capital synchronously.
        /// </summary>
        /// <param name="capital">Capital name.</param>
        /// <returns>Information about the country as <see cref="CountryInfo"/>>.</returns>
        /// <exception cref="ArgumentException">Throw if the capital name is null, empty, whitespace or nonexistent.</exception>
        CountryInfo GetCountryInfoByCapital(string? capital);

        /// <summary>
        /// Gets information about the currency by the country capital asynchronously.
        /// </summary>
        /// <param name="capital">Capital name.</param>
        /// <param name="token">Token for cancellation asynchronous operation.</param>
        /// <returns>Information about the country as <see cref="CountryInfo"/>>.</returns>
        /// <exception cref="ArgumentException">Throw if the capital name is null, empty, whitespace or nonexistent.</exception>
        Task<CountryInfo> GetCountryInfoByCapitalAsync(string? capital, CancellationToken token);
    }
}
