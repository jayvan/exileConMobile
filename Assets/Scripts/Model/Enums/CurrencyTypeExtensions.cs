public static class EquipmentTypeExtensions {
  public static DamageSet QualityDamageSet(this EquipmentType equipmentType) {
    switch (equipmentType) {
      case EquipmentType.TwoHand:
      case EquipmentType.OneHand:
        return new DamageSet{Physical = 1};
      case EquipmentType.Body:
      case EquipmentType.Shield:
        return new DamageSet{Block = 1};
    }

    return new DamageSet();
  }
}

public static class CurrencyTypeExtensions {
  public static string Name(this CurrencyType type) {
    return Data.Translations.Get("currency." + type + ".name").Value;
  }

  public static string Description(this CurrencyType type) {
    return Data.Translations.Get("currency." + type + ".description").Value;
  }
}
