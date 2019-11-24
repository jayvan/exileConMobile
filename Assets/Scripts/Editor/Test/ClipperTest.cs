using System.Collections.Generic;
using NUnit.Framework;

[Timeout(1000)]
public class ClipperTest {
  [Test]
  public void ClipAllTwice() {
    Equipment itemOne = new Equipment(2);
    Equipment itemTwo = new Equipment(2);
    Equipment itemThree = new Equipment(1);

    var equipment = new List<Equipment> {itemOne, itemTwo, itemThree};

    Clipper clipper = new Clipper(ClipStrategy.CLIP_ALL, 2);

    Dictionary<Equipment, int> result = clipper.Clip(equipment);
    Assert.AreEqual(2, result[itemOne]);
    Assert.AreEqual(2, result[itemTwo]);
    Assert.AreEqual(1, result[itemThree]);
  }

  [Test]
  public void ClipAllOnce() {
    Equipment itemOne = new Equipment(2);
    Equipment itemTwo = new Equipment(2);
    Equipment itemThree = new Equipment(1);

    var equipment = new List<Equipment> {itemOne, itemTwo, itemThree};

    Clipper clipper = new Clipper(ClipStrategy.CLIP_ALL, 1);

    Dictionary<Equipment, int> result = clipper.Clip(equipment);
    Assert.AreEqual(1, result[itemOne]);
    Assert.AreEqual(1, result[itemTwo]);
    Assert.AreEqual(1, result[itemThree]);
  }

  [Test]
  public void ClipNoRepeats() {
    Equipment itemOne = new Equipment(EquipmentType.ONE_HAND, Rarity.RARE, 2);
    Equipment itemTwo = new Equipment(EquipmentType.SHIELD, Rarity.MAGIC, 2);
    Equipment itemThree = new Equipment(EquipmentType.BODY, Rarity.NORMAL, 2);

    var equipment = new List<Equipment> {itemOne, itemTwo, itemThree};

    Clipper clipper = new Clipper(ClipStrategy.CLIP_DISTINCT, 2, new[] {ClipPriority.MOST_RARE});

    Dictionary<Equipment, int> result = clipper.Clip(equipment);
    Assert.AreEqual(1, result[itemOne]);
    Assert.AreEqual(1, result[itemTwo]);
    Assert.AreEqual(2, result.Count);
  }

  [Test]
  public void ClipRepeats() {
    Equipment itemOne = new Equipment(EquipmentType.ONE_HAND, Rarity.RARE, 2);
    Equipment itemTwo = new Equipment(EquipmentType.SHIELD, Rarity.MAGIC, 2);
    Equipment itemThree = new Equipment(EquipmentType.BODY, Rarity.NORMAL, 2);

    var equipment = new List<Equipment> {itemOne, itemTwo, itemThree};

    Clipper clipper = new Clipper(ClipStrategy.CLIP_REPEAT, 3, new[] {ClipPriority.MOST_RARE});

    Dictionary<Equipment, int> result = clipper.Clip(equipment);
    Assert.AreEqual(2, result[itemOne]);
    Assert.AreEqual(1, result[itemTwo]);
    Assert.AreEqual(2, result.Count);
  }
}
