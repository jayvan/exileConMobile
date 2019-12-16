using System;
using System.Collections.Generic;
using System.Linq;

public class ModPool {
  private Dictionary<ValueTuple<EquipmentType, Rarity>, List<Modifier>> mods = new Dictionary<ValueTuple<EquipmentType, Rarity>, List<Modifier>>();

  public Modifier GetMod(EquipmentType equipmentType, Rarity rarity, DamageSetConfig previous = null) {
    List<Modifier> list = this.mods[new ValueTuple<EquipmentType, Rarity>(equipmentType, rarity)];
    return list.First(a => a.DamageSetConfig != previous);
  }

  public void AddMod(EquipmentType equipmentType, Rarity rarity, DamageSetConfig damageSetConfig) {
    AddMod(new Modifier(equipmentType, rarity, damageSetConfig));
  }

  public void AddMod(Modifier modifier) {
      var key = new ValueTuple<EquipmentType, Rarity>(modifier.EquipmentType, modifier.Rarity);
      if (!mods.ContainsKey(key)) {
        mods.Add(key, new List<Modifier>());
      }

      mods[key].Add(modifier);
  }
}

public static class Data {
  public static ReferenceDepot<Translation> Translations;
  public static ReferenceDepot<DamageSetConfig> DamageSets;
  public static ReferenceDepot<BaseEquipment> BaseEquipments;
  public static ReferenceDepot<Modifier> Modifiers;
  public static ModPool ModPool = new ModPool();

  public static void Load() {
    Translations = ReferenceDepot<Translation>.Load(values => new Translation(values));
    DamageSets = ReferenceDepot<DamageSetConfig>.Load(values => new DamageSetConfig(values, Translations.Get(values[0], values[0].IndexOf("unq") > -1)));
    BaseEquipments = ReferenceDepot<BaseEquipment>.Load(values => new BaseEquipment(values, DamageSets.Get(values[0]).DamageSet, DamageSets.Get(values[0] + "_2", values[0].IndexOf("unq") >= -1)?.DamageSet ?? new DamageSet(), Translations.Get(values[0]).Value));
    Modifiers = ReferenceDepot<Modifier>.Load(values => new Modifier(values, DamageSets.Get(values[0])));

    Dictionary<ValueTuple<EquipmentType, Rarity>, List<Modifier>> mods = new Dictionary<ValueTuple<EquipmentType, Rarity>, List<Modifier>>();

    foreach (Modifier modifier in Modifiers.All()) {
      ModPool.AddMod(modifier);
    }
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

  public Modifier(EquipmentType type, Rarity rarity, DamageSetConfig damageSetConfig) {
    this.DamageSetConfig = damageSetConfig;
    this.Rarity = rarity;
    this.EquipmentType = type;
    this.Reference = this.DamageSetConfig.Reference + this.EquipmentType;
  }
}
