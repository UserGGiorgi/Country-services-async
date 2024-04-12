using CountryServices.Tests.Comparers;
using NUnit.Framework;

namespace CountryServices.Tests;

[TestFixture]
public class CountryCurrencyServiceTests
{
    private const string ServiceUrl = "https://restcountries.com/v2";
    private CountryService countryService;

    [OneTimeSetUp]
    public void Setup()
    {
        this.countryService = new CountryService(ServiceUrl);
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        this.countryService.Dispose();
    }

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCurrency))]
    public void GetLocalCurrencyByAlpha2Or3CodeValidCountryCode(string countryCode, LocalCurrency expected)
    {
        var comparer = new LocalCurrencyComparer();
        var actual = this.countryService.GetLocalCurrencyByAlpha2Or3Code(countryCode);
        Assert.IsTrue(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetLocalCurrencyByAlpha2Or3CodeInvalidCountryCodeThrowArgumentException(string? countryCode)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CountryService(ServiceUrl).GetLocalCurrencyByAlpha2Or3Code(countryCode);
        });
    }

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCurrency))]
    public async Task GetLocalCurrencyByAlpha2Or3CodeAsyncValidCountryCode(string countryCode, LocalCurrency? expected)
    {
        var comparer = new LocalCurrencyComparer();
        var actual = await this.countryService.GetLocalCurrencyByAlpha2Or3CodeAsync(countryCode, CancellationToken.None);
        Assert.IsTrue(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetLocalCurrencyByAlpha2Or3CodeAsyncInvalidCountryCodeThrowArgumentException(string? countryCode)
    {
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            _ = await new CountryService(ServiceUrl).GetLocalCurrencyByAlpha2Or3CodeAsync(countryCode, CancellationToken.None);
        });
    }

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCountryInfo))]
    public void GetCountryInfoByCapitalValidCapitalName(string capitalName, CountryInfo expected)
    {
        var comparer = new CountryInfoComparer();
        var actual = this.countryService.GetCountryInfoByCapital(capitalName);
        Assert.IsTrue(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetCountryInfoByCapitalInvalidCapitalNameThrowArgumentException(string? capitalName)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CountryService(ServiceUrl).GetCountryInfoByCapital(capitalName);
        });
    }

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCountryInfo))]
    public async Task GetCountryInfoByCapitalAsyncValidCapitalName(string capitalName, CountryInfo expected)
    {
        var comparer = new CountryInfoComparer();
        var actual = await this.countryService.GetCountryInfoByCapitalAsync(capitalName, CancellationToken.None);
        Assert.IsTrue(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetCountryInfoByCapitalAsyncInvalidCapitalNameThrowArgumentException(string? capitalName)
    {
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            _ = await new CountryService(ServiceUrl).GetCountryInfoByCapitalAsync(capitalName, CancellationToken.None);
        });
    }
}
