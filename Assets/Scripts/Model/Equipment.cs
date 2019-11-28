using System;

public class Equipment {
  public BaseEquipment Base { get; private set; }
  public int Damage { get; private set; }
  public Rarity Rarity { get; private set; }
  public EquipmentType EquipmentType => this.Base.EquipmentType;

  public DamageSet DamageTypes {
    get {
      return this.qualityDamageTypes + this.BaseDamageTypes + this.rolledDamageTypes;
    }
  }

  public string Name => Base.Name;
  public string ModifierName => string.Empty;

  public DamageSet BaseDamageTypes => this.Base.DamageSet;
  public DamageSet QualityDamageTypes => this.qualityDamageTypes;
  public DamageSet RolledDamageTypes => this.rolledDamageTypes;

  private DamageSet qualityDamageTypes = new DamageSet();
  private DamageSet rolledDamageTypes = new DamageSet();

  public Equipment(BaseEquipment baseEquipment) {
    this.Base = baseEquipment;
  }

  public Equipment(EquipmentType type, DamageSet baseDamage) {
    this.Base = new BaseEquipment(type, baseDamage);
  }

  public Equipment(EquipmentType type, Rarity rarity, int damage) {
    this.Base = new BaseEquipment(type, new DamageSet());
    this.Rarity = rarity;
    this.Damage = damage;
  }

  public Equipment(EquipmentType type) {
    this.Base = new BaseEquipment(type, new DamageSet());
  }

  public Equipment(Rarity rarity) {
    this.Rarity = rarity;
  }

  public Equipment(int damage) {
    this.Damage = damage;
  }
}
