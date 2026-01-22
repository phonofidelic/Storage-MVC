using System;

namespace Storage.Core.ValueObjects;

public class Currency
{
    public string EnglishName { get; set; }
    public string NativeName { get; set; }
    public string Symbol { get; set; }
    public string ISOSymbol { get; set; }

    public Currency(
        string englishName,
        string nativeName,
        string symbol,
        string isoSymbol
    )
    {
        EnglishName = englishName;
        NativeName = nativeName;
        Symbol = symbol;
        ISOSymbol = isoSymbol;
    }
}
