using System;

public class BaseEquipment : ReferenceData {
  public readonly bool Unique;
  public readonly EquipmentType EquipmentType;
  public readonly DamageSet DamageSet;
  public readonly DamageSet SecondaryDamageSet;
  public readonly string Name;

  public BaseEquipment(string[] fields, DamageSet damageSet, DamageSet secondaryDamageSet, string name) {
    this.Reference = fields[0];
    this.Unique = fields[1] != "0";
    this.DamageSet = damageSet;
    this.SecondaryDamageSet = secondaryDamageSet;
    this.Name = name;
    this.EquipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), fields[2]);
  }

  public BaseEquipment(EquipmentType type, DamageSet damage) {
    this.EquipmentType = type;
    this.DamageSet = damage;
  }
}
