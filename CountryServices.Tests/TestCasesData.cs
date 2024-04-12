using System.Collections.Generic;
using NUnit.Framework;

namespace CountryServices.Tests
{
    public sealed class TestCasesData
    {
        public static IEnumerable<TestCaseData> TestCasesForCurrency
        {
            get
            {
                yield return new TestCaseData(
                    "br",
                    new LocalCurrency { CountryName = "Brazil", CurrencyCode = "BRL", CurrencySymbol = "R$", });
                yield return new TestCaseData(
                    "gb",
                    new LocalCurrency
                    {
                        CountryName = "United Kingdom of Great Britain and Northern Ireland",
                        CurrencyCode = "GBP",
                        CurrencySymbol = "£",
                    });
                yield return new TestCaseData(
                    "US",
                    new LocalCurrency
                    {
                        CountryName = "United States of America", CurrencyCode = "USD", CurrencySymbol = "$",
                    });
                yield return new TestCaseData(
                    "pl",
                    new LocalCurrency { CountryName = "Poland", CurrencyCode = "PLN", CurrencySymbol = "zł", });
                yield return new TestCaseData(
                    "DE",
                    new LocalCurrency { CountryName = "Germany", CurrencyCode = "EUR", CurrencySymbol = "€", });
                yield return new TestCaseData(
                    "fr",
                    new LocalCurrency { CountryName = "France", CurrencyCode = "EUR", CurrencySymbol = "€", });
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForCountryInfo
        {
            get
            {
                yield return new TestCaseData(
                    "Brasília",
                    new CountryInfo
                    {
                        Name = "Brazil",
                        CapitalName = "Brasília",
                        Area = 8515767.0,
                        Population = 212559409,
                        Flag = "https://flagcdn.com/br.svg",
                    });
                yield return new TestCaseData(
                    "London",
                    new CountryInfo
                    {
                        Name = "United Kingdom of Great Britain and Northern Ireland",
                        CapitalName = "London",
                        Area = 242900.0,
                        Population = 67215293,
                        Flag = "https://flagcdn.com/w320/gb.png",
                    });
                yield return new TestCaseData(
                    "Warsaw",
                    new CountryInfo
                    {
                        Name = "Poland",
                        CapitalName = "Warsaw",
                        Area = 312679.0,
                        Population = 37950802,
                        Flag = "https://flagcdn.com/w320/pl.png",
                    });
                yield return new TestCaseData(
                    "Minsk",
                    new CountryInfo
                    {
                        Name = "Belarus",
                        CapitalName = "Minsk",
                        Area = 207600.0,
                        Population = 9398861,
                        Flag = "https://flagcdn.com/w320/by.png",
                    });
            }
        }
    }
}
