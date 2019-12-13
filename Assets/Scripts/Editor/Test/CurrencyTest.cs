using NUnit.Framework;

[Timeout(1000)]
public class CurrencyTest {
  [Test]
  public void WhetstoneDoesntWorkOnArmour() {
    Equipment armour = new Equipment(EquipmentType.Body);
    Assert.False(armour.CanUseCurrency(CurrencyType.Whetstone));
  }

  [Test]
  public void WhetstoneAddsQualityToOneHanders() {
    Equipment weapon = new Equipment(EquipmentType.OneHand);
    Assert.True(weapon.UseCurrency(CurrencyType.Whetstone));
    Assert.True(weapon.HasQuality);
  }

  [Test]
  public void WhetstoneAddsQualityToTwoHanders() {
    Equipment weapon = new Equipment(EquipmentType.TwoHand);
    Assert.True(weapon.UseCurrency(CurrencyType.Whetstone));
    Assert.True(weapon.HasQuality);
  }

  [Test]
  public void WhetstoneCantBeUsedOnQualitiedItem() {
    Equipment weapon = new Equipment(EquipmentType.TwoHand);
    weapon.UseCurrency(CurrencyType.Whetstone);
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
    Assert.True(body.UseCurrency(CurrencyType.Scrap));
    Assert.True(body.HasQuality);
  }

  [Test]
  public void ScrapAddsQualityToShield() {
    Equipment shield = new Equipment(EquipmentType.Shield);
    Assert.True(shield.UseCurrency(CurrencyType.Scrap));
    Assert.True(shield.HasQuality);
  }

  [Test]
  public void ScrapCantBeUsedOnQualitiedItem() {
    Equipment shield = new Equipment(EquipmentType.Shield);
    shield.UseCurrency(CurrencyType.Scrap);
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
    Assert.True(blue.UseCurrency(CurrencyType.Scour));
    Assert.IsNull(blue.RolledMod);
    Assert.AreEqual(Rarity.Normal, blue.Rarity);
  }

  [Test]
  public void ScourRemovesModsFromRareItems() {
    Equipment yellow = new Equipment(Rarity.Rare);
    yellow.SetRolledMod(new DamageSetConfig(new DamageSet{Physical = 3}), Rarity.Rare);
    Assert.True(yellow.UseCurrency(CurrencyType.Scour));
    Assert.IsNull(yellow.RolledMod);
    Assert.AreEqual(Rarity.Normal, yellow.Rarity);
  }
}
