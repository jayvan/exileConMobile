using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Equipment {
  public int Durability { get; private set; }
  public Rarity Rarity { get; private set; }
  public EquipmentType EquipmentType { get; private set; }

  public Dictionary<DamageType, int> DamageTypes {
    get {
      var damageTypes = new Dictionary<DamageType, int>();

      foreach (KeyValuePair<DamageType, int> kvp in BaseDamageTypes.Concat(QualityDamageTypes).Concat(RolledDamageTypes)) {
        if (!damageTypes.ContainsKey(kvp.Key)) {
          damageTypes.Add(kvp.Key, kvp.Value);
        } else {
          damageTypes[kvp.Key] += kvp.Value;
        }
      }

      return damageTypes;
    }
  }

  public ReadOnlyDictionary<DamageType, int> BaseDamageTypes => new ReadOnlyDictionary<DamageType, int>(this.baseDamageTypes);
  public ReadOnlyDictionary<DamageType, int> QualityDamageTypes => new ReadOnlyDictionary<DamageType, int>(this.qualityDamageTypes);
  public ReadOnlyDictionary<DamageType, int> RolledDamageTypes => new ReadOnlyDictionary<DamageType, int>(this.rolledDamageTypes);

  private Dictionary<DamageType, int> baseDamageTypes = new Dictionary<DamageType, int>();
  private Dictionary<DamageType, int> qualityDamageTypes = new Dictionary<DamageType, int>();
  private Dictionary<DamageType, int> rolledDamageTypes = new Dictionary<DamageType, int>();

  public Equipment(EquipmentType type, Dictionary<DamageType, int> baseDamageTypes) {
    this.EquipmentType = type;
    this.baseDamageTypes = baseDamageTypes;
  }

  public Equipment(EquipmentType type, Rarity rarity, int durability) {
    this.EquipmentType = type;
    this.Rarity = rarity;
    this.Durability = durability;
  }

  public Equipment(EquipmentType type) {
    this.EquipmentType = type;
  }

  public Equipment(Rarity rarity) {
    this.Rarity = rarity;
  }

  public Equipment(int durability) {
    this.Durability = durability;
  }
}
