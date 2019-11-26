using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Timeout(1000)]
public class CombatTest {
  private Enemy enemyOne;
  private Enemy enemyTwo;
  private Enemy enemyThree;
  private Enemy enemyFour;

  [OneTimeSetUp]
  public void Setup() {
    var enemyOneAttack = new DamageSet {
      Fire = 3
    };
    var enemyOneDefense = new DamageSet {
      Cold = 3
    };

    this.enemyOne = new Enemy(enemyOneAttack, enemyOneDefense);

    var enemyTwoAttack = new DamageSet {
      Fire = 3
    };
    var enemyTwoDefense = new DamageSet {
      Cold = 3,
      Extra = 2
    };

    this.enemyTwo = new Enemy(enemyTwoAttack, enemyTwoDefense);

    var enemyThreeAttack = new DamageSet {
      Fire = 3,
      Extra = 2
    };
    var enemyThreeDefense = new DamageSet {
      Cold = 3
    };

    this.enemyThree = new Enemy(enemyThreeAttack, enemyThreeDefense);

    var enemyFourAttack = new DamageSet {
      Block = 5,
      Extra = 2
    };
    var enemyFourDefense = new DamageSet {
      Physical = 5,
      Extra = 2
    };

    this.enemyFour = new Enemy(enemyFourAttack, enemyFourDefense);
  }

  [Test]
  public void ExactlyMeetingFixedDamageTypeRequirementsWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyOne.Defense),
      new Equipment(EquipmentType.SHIELD, this.enemyOne.Attack)
    };

    Assert.True(this.enemyOne.CanDefeatWith(gear));
  }

  [Test]
  public void TooLowAttackDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet{Cold = 2}),
      new Equipment(EquipmentType.SHIELD, this.enemyOne.Attack)
    };


    Assert.False(this.enemyOne.CanDefeatWith(gear));
  }

  [Test]
  public void TooLowDefenseDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyOne.Defense),
      new Equipment(EquipmentType.SHIELD, new DamageSet{Fire = 2})
    };


    Assert.False(this.enemyOne.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraAttackOfMainTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet{Cold = 5}),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.True(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraAttackOfSecondaryTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet {
        Cold = 3,
        Physical = 2
      }),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.True(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraAttackOfMultipleTypesWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet {
        Cold = 3,
        Physical = 1,
        Lightning = 1
      }),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.True(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void LackingExtraAttackDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet{Cold = 3}),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.False(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraDefenseOfMainTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new DamageSet{Fire = 5})
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraDefenseOfSecondaryTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new DamageSet {
        Fire = 3,
        Block = 2
      })
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraDefenseOfMultipleTypesWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new DamageSet {
        Fire = 3,
        Block = 1,
        Lightning = 1
      })
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void LackingExtraDefenseDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new DamageSet {
        Fire = 3
      })
    };

    Assert.False(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void WildsOnPlayerGear() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet { Fire = 2, Wild = 2, Cold = 1 }),
      new Equipment(EquipmentType.SHIELD, new DamageSet { Wild = 2, Fire = 2, Lightning = 1})
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraTypedDamageDoesNotCountAsWild() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new DamageSet { Cold = 2, Lightning = 3}),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.False(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void JewelryProvidesPhysicalAndBlock() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.RING, new DamageSet {Physical = 1, Block = 1}),
      new Equipment(EquipmentType.RING, new DamageSet {Physical = 1, Block = 1}),
      new Equipment(EquipmentType.AMULET, new DamageSet {Physical = 3, Block = 3}),
      new Equipment(EquipmentType.BODY, new DamageSet {Cold = 2 }),
      new Equipment(EquipmentType.ONE_HAND, new DamageSet {Cold = 2 })
    };

    Assert.True(this.enemyFour.CanDefeatWith(gear));
  }

  [Test]
  public void JewelryPhysicalAndBlockIsNotDoubleCounted() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.RING, new DamageSet {Physical = 1, Block = 1}),
      new Equipment(EquipmentType.RING, new DamageSet {Physical = 1, Block = 1}),
      new Equipment(EquipmentType.AMULET, new DamageSet {Physical = 3, Block = 3}),
    };

    Assert.False(this.enemyFour.CanDefeatWith(gear));
  }

  [Test]
  public void JewelryCanSatisfyStats() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.RING, new DamageSet {Cold = 1, Fire = 1}),
      new Equipment(EquipmentType.RING, new DamageSet {Cold = 1, Fire = 1}),
      new Equipment(EquipmentType.AMULET, new DamageSet {Cold = 1, Fire = 1}),
    };

    Assert.True(this.enemyOne.CanDefeatWith(gear));
  }

  [Test]
  public void JewelryCanBeSplitAcrossAttackAndDefense() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.RING, new DamageSet {Fire = 5, Cold = 3})
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void JewelryWildWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.RING, new DamageSet {Wild = 8})
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void JewelryLackingWildDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.RING, new DamageSet {Wild = 7})
    };

    Assert.False(this.enemyThree.CanDefeatWith(gear));
  }
}
