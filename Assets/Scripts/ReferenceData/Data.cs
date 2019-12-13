using System;
using System.Collections.Generic;
using System.Linq;

public static class Data {
  public static ReferenceDepot<Translation> Translations;
  public static ReferenceDepot<DamageSetConfig> DamageSets;
  public static ReferenceDepot<BaseEquipment> BaseEquipments;
  public static ReferenceDepot<Modifier> Modifiers;
  private static Dictionary<ValueTuple<EquipmentType, Rarity>, List<Modifier>> modPool = new Dictionary<ValueTuple<EquipmentType, Rarity>, List<Modifier>>();

  public static void Load() {
    Translations = ReferenceDepot<Translation>.Load(values => new Translation(values));
    DamageSets = ReferenceDepot<DamageSetConfig>.Load(values => new DamageSetConfig(values, Translations.Get(values[0], values[0].IndexOf("unq") > -1)));
    BaseEquipments = ReferenceDepot<BaseEquipment>.Load(values => new BaseEquipment(values, DamageSets.Get(values[0]).DamageSet, DamageSets.Get(values[0] + "_2", values[0].IndexOf("unq") >= -1)?.DamageSet ?? new DamageSet(), Translations.Get(values[0]).Value));
    Modifiers = ReferenceDepot<Modifier>.Load(values => new Modifier(values, DamageSets.Get(values[0])));

    foreach (Modifier modifier in Modifiers.All()) {
      var key = new ValueTuple<EquipmentType, Rarity>(modifier.EquipmentType, modifier.Rarity);
      if (!modPool.ContainsKey(key)) {
        modPool.Add(key, new List<Modifier>());
      }

      modPool[key].Add(modifier);
    }
  }

  public static Modifier GetMod(EquipmentType equipmentType, Rarity rarity, DamageSetConfig previous = null) {
    List<Modifier> list = modPool[new ValueTuple<EquipmentType, Rarity>(equipmentType, rarity)];
    return list.First(a => a.DamageSetConfig != previous);
  }
}

public class Modifier : ReferenceData {
  public readonly DamageSetConfig DamageSetConfig;
  public readonly EquipmentType EquipmentType;
  public readonly Rarity Rarity;

  public Modifier(string[] values, DamageSetConfig damageSet) {
    this.Reference = values[0] + values[1];
    this.DamageSetConfig = damageSet;
    this.EquipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), values[1]);
    this.Rarity = (Rarity)Enum.Parse(typeof(Rarity), values[2]);
  }
}
