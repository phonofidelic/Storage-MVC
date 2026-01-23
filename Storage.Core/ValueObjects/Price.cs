using System;
using System.Globalization;
using Storage.Core.ValueObjects;

namespace Storage.Core.Entities;

public abstract class Price
{
    public decimal Value { get; set; }
    public Currency Currency { get; set; } = new(
        englishName: RegionInfo.CurrentRegion.CurrencyEnglishName,
        nativeName: RegionInfo.CurrentRegion.CurrencyNativeName,
        symbol: RegionInfo.CurrentRegion.CurrencySymbol,
        isoSymbol: RegionInfo.CurrentRegion.ISOCurrencySymbol);
}
