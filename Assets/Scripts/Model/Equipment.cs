public class Equipment {
  public int Durability { get; private set; }
  public Rarity Rarity { get; private set; }
  public EquipmentType EquipmentType { get; private set; }

  public DamageSet DamageTypes {
    get {
      return this.qualityDamageTypes + this.baseDamageTypes + this.rolledDamageTypes;
    }
  }

  public DamageSet BaseDamageTypes => this.baseDamageTypes;
  public DamageSet QualityDamageTypes => this.qualityDamageTypes;
  public DamageSet RolledDamageTypes => this.rolledDamageTypes;

  private DamageSet baseDamageTypes = new DamageSet();
  private DamageSet qualityDamageTypes = new DamageSet();
  private DamageSet rolledDamageTypes = new DamageSet();

  public Equipment(EquipmentType type, DamageSet baseDamage) {
    this.EquipmentType = type;
    this.baseDamageTypes = baseDamage;
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
