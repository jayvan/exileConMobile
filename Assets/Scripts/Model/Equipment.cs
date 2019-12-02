public class Equipment {
  public BaseEquipment Base { get; private set; }
  public int Damage { get; private set; }
  public Rarity Rarity => this.Base.Unique ? Rarity.Unique : this.rarity;
  public EquipmentType EquipmentType => this.Base.EquipmentType;
  public DamageSetConfig RolledMod { get; private set; }

  public DamageSet DamageTypes => this.qualityDamageTypes + this.BaseDamageTypes + this.RolledDamageTypes;

  public string Name => Base.Name;
  public string ModifierName => RolledMod == null ? null : RolledMod.Translation;

  public DamageSet BaseDamageTypes => this.Base.DamageSet;
  public DamageSet RolledDamageTypes {
    get {
      if (this.Base.Unique) {
        return this.Base.SecondaryDamageSet;
      }

      return this.RolledMod?.DamageSet ?? new DamageSet();
    }
  }

  public DamageSet QualityDamageTypes => this.qualityDamageTypes;

  private DamageSet qualityDamageTypes = new DamageSet();
  private Rarity rarity;

  public Equipment(BaseEquipment baseEquipment) {
    this.Base = baseEquipment;
  }

  public Equipment(EquipmentType type, DamageSet baseDamage) {
    this.Base = new BaseEquipment(type, baseDamage);
  }

  public Equipment(EquipmentType type, Rarity rarity, int damage) {
    this.Base = new BaseEquipment(type, new DamageSet());
    this.rarity = rarity;
    this.Damage = damage;
  }

  public Equipment(EquipmentType type) {
    this.Base = new BaseEquipment(type, new DamageSet());
  }

  public Equipment(Rarity rarity) {
    this.rarity = rarity;
  }

  public Equipment(int damage) {
    this.Damage = damage;
  }

  public void SetRolledMod(DamageSetConfig rolledMod, Rarity rarity) {
    this.RolledMod = rolledMod;
    this.rarity = rarity;
  }

  public void RemoveDurability() {
    this.Damage++;
  }
}
