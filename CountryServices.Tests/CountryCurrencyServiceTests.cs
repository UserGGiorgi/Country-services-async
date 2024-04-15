using CountryServices.Tests.Comparators;
using NUnit.Framework;

namespace CountryServices.Tests;

[TestFixture]
public class CountryCurrencyServiceTests
{
    private CountryService countryService = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        this.countryService = new CountryService();
    }

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCurrency))]
    public void GetLocalCurrencyByAlpha2Or3Code_ValidCountryCode(string countryCode, LocalCurrency expected)
    {
        var comparer = new LocalCurrencyComparer();
        var actual = this.countryService.GetLocalCurrencyByAlpha2Or3Code(countryCode);
        Assert.That(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetLocalCurrencyByAlpha2Or3Code_InvalidCountryCode_ThrowArgumentException(string? countryCode)
        => Assert.Throws<ArgumentException>(() =>
        {
            _ = new CountryService().GetLocalCurrencyByAlpha2Or3Code(countryCode);
        });

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCurrency))]
    public async Task GetLocalCurrencyByAlpha2Or3CodeAsync_ValidCountryCode(string? countryCode, LocalCurrency? expected)
    {
        var comparer = new LocalCurrencyComparer();
        var actual =
            await this.countryService.GetLocalCurrencyByAlpha2Or3CodeAsync(countryCode, CancellationToken.None);
        Assert.That(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetLocalCurrencyByAlpha2Or3CodeAsync_InvalidCountryCode_ThrowArgumentException(string? countryCode)
        => Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            _ = await new CountryService().GetLocalCurrencyByAlpha2Or3CodeAsync(countryCode, CancellationToken.None);
        });

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCountryInfo))]
    public void GetCountryInfoByCapital_ValidCapitalName(string capitalName, Country expected)
    {
        var comparer = new CountryInfoComparer();
        var actual = this.countryService.GetCountryInfoByCapital(capitalName);
        Assert.That(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetCountryInfoByCapital_InvalidCapitalName_ThrowArgumentException(string? capitalName)
        => Assert.Throws<ArgumentException>(() =>
        {
            _ = new CountryService().GetCountryInfoByCapital(capitalName);
        });

    [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCountryInfo))]
    public async Task GetCountryInfoByCapitalAsync_ValidCapitalName(string capitalName, Country expected)
    {
        var comparer = new CountryInfoComparer();
        var actual = await this.countryService.GetCountryInfoByCapitalAsync(capitalName, CancellationToken.None);
        Assert.That(comparer.Equals(expected, actual));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("UPSS")]
    public void GetCountryInfoByCapitalAsync_InvalidCapitalName_ThrowArgumentException(string? capitalName)
        => Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            _ = await new CountryService().GetCountryInfoByCapitalAsync(capitalName, CancellationToken.None);
        });
}
