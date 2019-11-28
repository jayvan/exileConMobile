using System;

public class BaseEquipment : ReferenceData {
  public readonly bool Unique;
  public readonly EquipmentType EquipmentType;
  public readonly DamageSet DamageSet;
  public readonly string Name;

  public BaseEquipment(string[] fields, DamageSet damageSet, string name) {
    this.Reference = fields[0];
    this.Unique = fields[1] != "0";
    this.DamageSet = damageSet;
    this.Name = name;
  }

  public BaseEquipment(EquipmentType type, DamageSet damage) {
    this.EquipmentType = type;
    this.DamageSet = damage;
  }
}
