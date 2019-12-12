public class Equipment {
  public BaseEquipment Base { get; private set; }
  public int Damage { get; private set; }
  public bool HasQuality { get; private set; }
  public Rarity Rarity => this.Base.Unique ? Rarity.Unique : this.rarity;
  public EquipmentType EquipmentType => this.Base.EquipmentType;
  public DamageSetConfig RolledMod { get; private set; }

  public DamageSet DamageSet => this.QualityDamageSet + this.BaseDamageSet + this.RolledDamageSet;

  public string Name => Base.Name;
  public string ModifierName => RolledMod?.Translation;

  public DamageSet BaseDamageSet => this.Base.DamageSet;

  public DamageSet RolledDamageSet {
    get {
      if (this.Base.Unique) {
        return this.Base.SecondaryDamageSet;
      }

      return this.RolledMod?.DamageSet ?? new DamageSet();
    }
  }

  public SavedEquipment SavedEquipment =>
    new SavedEquipment {
      BaseReference = this.Base.Reference,
      Damage = this.Damage,
      Rarity = this.Rarity,
      RolledModReference = this.RolledMod?.Reference,
      HasQuality = this.HasQuality
    };

  public DamageSet QualityDamageSet => this.HasQuality ? this.EquipmentType.QualityDamageSet() : new DamageSet();
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

  public Equipment(BaseEquipment baseEquipment, Rarity rarity, DamageSetConfig rolledMod, int damage, bool hasQuality) {
    this.Base = baseEquipment;
    this.rarity = rarity;
    this.RolledMod = rolledMod;
    this.Damage = damage;
    this.HasQuality = hasQuality;
  }

  public void SetRolledMod(DamageSetConfig rolledMod, Rarity rarity) {
    this.RolledMod = rolledMod;
    this.rarity = rarity;
  }

  public void RemoveDurability() {
    this.Damage++;
  }

  public bool UseCurrency(CurrencyType currency) {
    if (!CanUseCurrency(currency)) {
      return false;
    }

    switch (currency) {
      case CurrencyType.Scrap:
      case CurrencyType.Whetstone:
        this.HasQuality = true;
        break;
      case CurrencyType.Transmute:
      case CurrencyType.Alteration:
        break;
      case CurrencyType.Chaos:
      case CurrencyType.Alchemy:
        break;
      case CurrencyType.Scour:
        this.SetRolledMod(null, Rarity.Normal);
        break;
    }

    return true;
  }

  public bool CanUseCurrency(CurrencyType currency) {
    switch (currency) {
      case CurrencyType.Scrap:
        return (this.EquipmentType == EquipmentType.Body || this.EquipmentType == EquipmentType.Shield) &&
               !this.HasQuality;
      case CurrencyType.Whetstone:
        return (this.EquipmentType == EquipmentType.OneHand || this.EquipmentType == EquipmentType.TwoHand) &&
               !this.HasQuality;
      case CurrencyType.Transmute:
      case CurrencyType.Alchemy:
        return this.Rarity == Rarity.Normal;
      case CurrencyType.Alteration:
        return this.Rarity == Rarity.Magic;
      case CurrencyType.Chaos:
        return this.Rarity == Rarity.Rare;
      case CurrencyType.Scour:
        return this.Rarity != Rarity.Normal && this.Rarity != Rarity.Unique;
    }
    return false;
  }
}
