public static class Data {
  public static ReferenceDepot<Translation> Translations;
  public static ReferenceDepot<DamageSetConfig> DamageSets;
  public static ReferenceDepot<BaseEquipment> BaseEquipments;

  public static void Load() {
    Translations = ReferenceDepot<Translation>.Load(values => new Translation(values));
    DamageSets = ReferenceDepot<DamageSetConfig>.Load(values => new DamageSetConfig(values, Translations.Get(values[0], values[0].IndexOf("unq") > -1)));
    BaseEquipments = ReferenceDepot<BaseEquipment>.Load(values => new BaseEquipment(values, DamageSets.Get(values[0]).DamageSet, DamageSets.Get(values[0] + "_2", values[0].IndexOf("unq") >= -1)?.DamageSet ?? new DamageSet(), Translations.Get(values[0]).Value));
  }
}
