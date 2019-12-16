using NUnit.Framework;

[Timeout(1000)]
public class CurrencyTest {
  private ModPool modPool;

  [OneTimeSetUp]
  public void SetUp() {
    this.modPool = new ModPool();
    this.modPool.AddMod(EquipmentType.Shield, Rarity.Magic, new DamageSetConfig(new DamageSet{Block = 2}));
    this.modPool.AddMod(EquipmentType.Shield, Rarity.Magic, new DamageSetConfig(new DamageSet{Lightning = 2}));
    this.modPool.AddMod(EquipmentType.Shield, Rarity.Rare, new DamageSetConfig(new DamageSet{Block = 3}));
    this.modPool.AddMod(EquipmentType.Shield, Rarity.Rare, new DamageSetConfig(new DamageSet{Lightning = 3}));
  }

  [Test]
  public void WhetstoneDoesntWorkOnArmour() {
    Equipment armour = new Equipment(EquipmentType.Body);
    Assert.False(armour.CanUseCurrency(CurrencyType.Whetstone));
  }

  [Test]
  public void WhetstoneAddsQualityToOneHanders() {
    Equipment weapon = new Equipment(EquipmentType.OneHand);
    Assert.True(weapon.UseCurrency(CurrencyType.Whetstone, this.modPool));
    Assert.True(weapon.HasQuality);
  }

  [Test]
  public void WhetstoneAddsQualityToTwoHanders() {
    Equipment weapon = new Equipment(EquipmentType.TwoHand);
    Assert.True(weapon.UseCurrency(CurrencyType.Whetstone, this.modPool));
    Assert.True(weapon.HasQuality);
  }

  [Test]
  public void WhetstoneCantBeUsedOnQualitiedItem() {
    Equipment weapon = new Equipment(EquipmentType.TwoHand);
    weapon.UseCurrency(CurrencyType.Whetstone, this.modPool);
    Assert.False(weapon.CanUseCurrency(CurrencyType.Whetstone));
  }

  [Test]
  public void ScrapDoesntWorkOnRing() {
    Equipment ring = new Equipment(EquipmentType.Ring);
    Assert.False(ring.CanUseCurrency(CurrencyType.Scrap));
  }

  [Test]
  public void ScrapAddsQualityToBody() {
    Equipment body = new Equipment(EquipmentType.Body);
    Assert.True(body.UseCurrency(CurrencyType.Scrap, this.modPool));
    Assert.True(body.HasQuality);
  }

  [Test]
  public void ScrapAddsQualityToShield() {
    Equipment shield = new Equipment(EquipmentType.Shield);
    Assert.True(shield.UseCurrency(CurrencyType.Scrap, this.modPool));
    Assert.True(shield.HasQuality);
  }

  [Test]
  public void ScrapCantBeUsedOnQualitiedItem() {
    Equipment shield = new Equipment(EquipmentType.Shield);
    shield.UseCurrency(CurrencyType.Scrap, this.modPool);
    Assert.False(shield.CanUseCurrency(CurrencyType.Scrap));
  }

  [Test]
  public void ScourCantBeUsedOnWhiteItems() {
    Equipment white = new Equipment(Rarity.Normal);
    Assert.False(white.CanUseCurrency(CurrencyType.Scour));
  }

  [Test]
  public void ScourRemovesModsFromBlueItems() {
    Equipment blue = new Equipment(Rarity.Magic);
    blue.SetRolledMod(new DamageSetConfig(new DamageSet{Physical = 2}), Rarity.Magic);
    Assert.True(blue.UseCurrency(CurrencyType.Scour, this.modPool));
    Assert.IsNull(blue.RolledMod);
    Assert.AreEqual(Rarity.Normal, blue.Rarity);
  }

  [Test]
  public void ScourRemovesModsFromRareItems() {
    Equipment yellow = new Equipment(Rarity.Rare);
    yellow.SetRolledMod(new DamageSetConfig(new DamageSet{Physical = 3}), Rarity.Rare);
    Assert.True(yellow.UseCurrency(CurrencyType.Scour, this.modPool));
    Assert.IsNull(yellow.RolledMod);
    Assert.AreEqual(Rarity.Normal, yellow.Rarity);
  }

  [Test]
  public void TransmuteAddsMagicModToWhite() {
    Equipment white = new Equipment(EquipmentType.Shield);
    Assert.True(white.UseCurrency(CurrencyType.Transmute, this.modPool));
    Assert.AreEqual(Rarity.Magic, white.Rarity);
    Assert.NotNull(white.RolledMod);
  }

  [Test]
  public void TransmuteDoesNotWorkOnNonWhite() {
    Equipment blue = new Equipment(Rarity.Magic);
    Assert.False(blue.CanUseCurrency(CurrencyType.Transmute));
    Equipment yellow = new Equipment(Rarity.Rare);
    Assert.False(yellow.CanUseCurrency(CurrencyType.Transmute));
    Equipment orange = new Equipment(Rarity.Unique);
    Assert.False(orange.CanUseCurrency(CurrencyType.Transmute));
  }

  [Test]
  public void AlterationRerollsBlue() {
    Equipment blue = new Equipment(EquipmentType.Shield);
    blue.UseCurrency(CurrencyType.Transmute, this.modPool);
    DamageSetConfig previousMod = blue.RolledMod;
    Assert.True(blue.UseCurrency(CurrencyType.Alteration, this.modPool));
    Assert.AreNotEqual(previousMod, blue.RolledMod);
  }

  [Test]
  public void AlchAddsRareModToWhite() {
    Equipment white = new Equipment(EquipmentType.Shield);
    Assert.True(white.UseCurrency(CurrencyType.Alchemy, this.modPool));
    Assert.AreEqual(Rarity.Rare, white.Rarity);
    Assert.NotNull(white.RolledMod);
  }

  [Test]
  public void AlchDoesNotWorkOnNonWhite() {
    Equipment blue = new Equipment(Rarity.Magic);
    Assert.False(blue.CanUseCurrency(CurrencyType.Alchemy));
    Equipment yellow = new Equipment(Rarity.Rare);
    Assert.False(yellow.CanUseCurrency(CurrencyType.Alchemy));
    Equipment orange = new Equipment(Rarity.Unique);
    Assert.False(orange.CanUseCurrency(CurrencyType.Alchemy));
  }

  [Test]
  public void ChaosRerollsYellow() {
    Equipment yellow = new Equipment(EquipmentType.Shield);
    yellow.UseCurrency(CurrencyType.Alchemy, this.modPool);
    DamageSetConfig previousMod = yellow.RolledMod;
    Assert.True(yellow.UseCurrency(CurrencyType.Chaos, this.modPool));
    Assert.AreNotEqual(previousMod, yellow.RolledMod);
  }
}
