public static class CurrencyTypeExtensions {
  public static string Name(this CurrencyType type) {
    return Data.Translations.Get("currency." + type + ".name").Value;
  }

  public static string Description(this CurrencyType type) {
    return Data.Translations.Get("currency." + type + ".description").Value;
  }
}
