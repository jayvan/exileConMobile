using System.Collections.Generic;
using NUnit.Framework;

[Timeout(1000)]
public class ClipTest {
  [Test]
  [Repeat(10)]
  public void HighestRarityComparer() {
    Equipment rare = new Equipment(Rarity.Rare);
    Equipment magic = new Equipment(Rarity.Magic);
    Equipment normal = new Equipment(Rarity.Normal);
    List<Equipment> equipment = new List<Equipment> {normal, magic, rare};
    equipment.Shuffle();
    equipment.Sort(new HighestRarityComparer());
    Assert.AreEqual(rare, equipment[0]);
    Assert.AreEqual(magic, equipment[1]);
    Assert.AreEqual(normal, equipment[2]);
  }

  [Test]
  [Repeat(10)]
  public void LowestRarityComparer() {
    Equipment rare = new Equipment(Rarity.Rare);
    Equipment magic = new Equipment(Rarity.Magic);
    Equipment normal = new Equipment(Rarity.Normal);
    List<Equipment> equipment = new List<Equipment> {normal, magic, rare};
    equipment.Shuffle();
    equipment.Sort(new LowestRarityComparer());
    Assert.AreEqual(normal, equipment[0]);
    Assert.AreEqual(magic, equipment[1]);
    Assert.AreEqual(rare, equipment[2]);
  }

  [Test]
  [Repeat(10)]
  public void MostDamaged() {
    Equipment perfect = new Equipment(0);
    Equipment damaged = new Equipment(1);
    List<Equipment> equipment = new List<Equipment> {perfect, damaged};
    equipment.Shuffle();
    equipment.Sort(new MostDamagedComparer());
    Assert.AreEqual(damaged, equipment[0]);
    Assert.AreEqual(perfect, equipment[1]);
  }

  [Test]
  [Repeat(10)]
  public void LeastDamaged() {
    Equipment perfect = new Equipment(0);
    Equipment damaged = new Equipment(1);
    List<Equipment> equipment = new List<Equipment> {perfect, damaged};
    equipment.Shuffle();
    equipment.Sort(new LeastDamagedComparer());
    Assert.AreEqual(perfect, equipment[0]);
    Assert.AreEqual(damaged, equipment[1]);
  }

  [Test]
  [Repeat(10)]
  public void Weapon() {
    Equipment oneHander = new Equipment(EquipmentType.OneHand);
    Equipment twoHander = new Equipment(EquipmentType.TwoHand);
    Equipment shield = new Equipment(EquipmentType.Shield);
    Equipment body = new Equipment(EquipmentType.Body);
    Equipment amulet = new Equipment(EquipmentType.Amulet);
    Equipment ring = new Equipment(EquipmentType.Ring);
    List<Equipment> equipment = new List<Equipment> {oneHander, twoHander, shield, body, amulet, ring};
    List<Equipment> valid = new List<Equipment> {oneHander, twoHander};
    equipment.Shuffle();
    equipment.Sort(new WeaponComparer());
    Assert.Contains(equipment[0], valid);
    Assert.Contains(equipment[1], valid);
    Assert.False(valid.Contains(equipment[2]));
  }

  [Test]
  [Repeat(10)]
  public void Shield() {
    Equipment oneHander = new Equipment(EquipmentType.OneHand);
    Equipment twoHander = new Equipment(EquipmentType.TwoHand);
    Equipment shield = new Equipment(EquipmentType.Shield);
    Equipment body = new Equipment(EquipmentType.Body);
    Equipment amulet = new Equipment(EquipmentType.Amulet);
    Equipment ring = new Equipment(EquipmentType.Ring);
    List<Equipment> equipment = new List<Equipment> {oneHander, twoHander, shield, body, amulet, ring};
    equipment.Shuffle();
    equipment.Sort(new ShieldComparer());
    Assert.AreEqual(equipment[0], shield);
  }

  [Test]
  [Repeat(10)]
  public void Body() {
    Equipment oneHander = new Equipment(EquipmentType.OneHand);
    Equipment twoHander = new Equipment(EquipmentType.TwoHand);
    Equipment shield = new Equipment(EquipmentType.Shield);
    Equipment body = new Equipment(EquipmentType.Body);
    Equipment amulet = new Equipment(EquipmentType.Amulet);
    Equipment ring = new Equipment(EquipmentType.Ring);
    List<Equipment> equipment = new List<Equipment> {oneHander, twoHander, shield, body, amulet, ring};
    equipment.Shuffle();
    equipment.Sort(new BodyComparer());
    Assert.AreEqual(equipment[0], body);
  }

  [Test]
  [Repeat(10)]
  public void Amulet() {
    Equipment oneHander = new Equipment(EquipmentType.OneHand);
    Equipment twoHander = new Equipment(EquipmentType.TwoHand);
    Equipment shield = new Equipment(EquipmentType.Shield);
    Equipment body = new Equipment(EquipmentType.Body);
    Equipment amulet = new Equipment(EquipmentType.Amulet);
    Equipment ring = new Equipment(EquipmentType.Ring);
    List<Equipment> equipment = new List<Equipment> {oneHander, twoHander, shield, body, amulet, ring};
    equipment.Shuffle();
    equipment.Sort(new AmuletComparer());
    Assert.AreEqual(equipment[0], amulet);
  }

  [Test]
  [Repeat(10)]
  public void Ring() {
    Equipment oneHander = new Equipment(EquipmentType.OneHand);
    Equipment twoHander = new Equipment(EquipmentType.TwoHand);
    Equipment shield = new Equipment(EquipmentType.Shield);
    Equipment body = new Equipment(EquipmentType.Body);
    Equipment amulet = new Equipment(EquipmentType.Amulet);
    Equipment ringA = new Equipment(EquipmentType.Ring);
    Equipment ringB = new Equipment(EquipmentType.Ring);
    List<Equipment> equipment = new List<Equipment> {oneHander, twoHander, shield, body, amulet, ringA, ringB};
    List<Equipment> valid = new List<Equipment> {ringA, ringB};
    equipment.Shuffle();
    equipment.Sort(new RingComparer());
    Assert.Contains(equipment[0], valid);
    Assert.Contains(equipment[1], valid);
    Assert.False(valid.Contains(equipment[2]));
  }

  [Test]
  [Repeat(10)]
  public void Jewelry() {
    Equipment oneHander = new Equipment(EquipmentType.OneHand);
    Equipment twoHander = new Equipment(EquipmentType.TwoHand);
    Equipment shield = new Equipment(EquipmentType.Shield);
    Equipment body = new Equipment(EquipmentType.Body);
    Equipment amulet = new Equipment(EquipmentType.Amulet);
    Equipment ringA = new Equipment(EquipmentType.Ring);
    Equipment ringB = new Equipment(EquipmentType.Ring);
    List<Equipment> equipment = new List<Equipment> {oneHander, twoHander, shield, body, amulet, ringA, ringB};
    List<Equipment> valid = new List<Equipment> {ringA, ringB, amulet};
    equipment.Shuffle();
    equipment.Sort(new JewelryComparer());
    Assert.Contains(equipment[0], valid);
    Assert.Contains(equipment[1], valid);
    Assert.Contains(equipment[2], valid);
    Assert.False(valid.Contains(equipment[3]));
  }

  [Test]
  [Repeat(10)]
  public void Composition() {
    // Jewelry > Rarity > Durability
    Equipment ringA = new Equipment(EquipmentType.Ring, Rarity.Rare, 2);
    Equipment ringB = new Equipment(EquipmentType.Ring, Rarity.Rare, 1);
    Equipment amulet = new Equipment(EquipmentType.Amulet, Rarity.Magic, 1);
    Equipment weapon = new Equipment(EquipmentType.OneHand, Rarity.Rare, 2);
    Equipment shield = new Equipment(EquipmentType.Shield, Rarity.Rare, 1);
    Equipment body = new Equipment(EquipmentType.Body, Rarity.Normal, 2);

    List<Equipment> equipment = new List<Equipment> {weapon, shield, body, amulet, ringA, ringB};
    equipment.Shuffle();

    var comparer = new JewelryComparer();
    comparer.SetNext(new HighestRarityComparer()).SetNext(new LeastDamagedComparer());

    equipment.Sort(comparer);
    Assert.AreEqual(ringB, equipment[0]);
    Assert.AreEqual(ringA, equipment[1]);
    Assert.AreEqual(amulet, equipment[2]);
    Assert.AreEqual(shield, equipment[3]);
    Assert.AreEqual(weapon, equipment[4]);
    Assert.AreEqual(body, equipment[5]);
  }
}
