using System.Collections.Generic;
using NUnit.Framework;

[Timeout(1000)]
public class CombatTest {
  private Enemy enemyOne;
  private Enemy enemyTwo;
  private Enemy enemyThree;

  [OneTimeSetUp]
  public void Setup() {
    var enemyOneAttack = new Dictionary<DamageType, int> {
      { DamageType.FIRE, 3}
    };
    var enemyOneDefense = new Dictionary<DamageType, int> {
      { DamageType.COLD, 3}
    };

    this.enemyOne = new Enemy(enemyOneAttack, enemyOneDefense);

    var enemyTwoAttack = new Dictionary<DamageType, int> {
      { DamageType.FIRE, 3}
    };
    var enemyTwoDefense = new Dictionary<DamageType, int> {
      { DamageType.COLD, 3},
      { DamageType.WILD, 2}
    };

    this.enemyTwo = new Enemy(enemyTwoAttack, enemyTwoDefense);

    var enemyThreeAttack = new Dictionary<DamageType, int> {
      { DamageType.FIRE, 3},
      { DamageType.WILD, 2}
    };
    var enemyThreeDefense = new Dictionary<DamageType, int> {
      { DamageType.COLD, 3}
    };

    this.enemyThree = new Enemy(enemyThreeAttack, enemyThreeDefense);
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
      new Equipment(EquipmentType.ONE_HAND, new Dictionary<DamageType, int>{{DamageType.COLD, 2}}),
      new Equipment(EquipmentType.SHIELD, this.enemyOne.Attack)
    };


    Assert.False(this.enemyOne.CanDefeatWith(gear));
  }

  [Test]
  public void TooLowDefenseDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyOne.Defense),
      new Equipment(EquipmentType.SHIELD, new Dictionary<DamageType, int>{{DamageType.FIRE, 2}})
    };


    Assert.False(this.enemyOne.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraAttackOfMainTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new Dictionary<DamageType, int>{{DamageType.COLD, 5}}),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.True(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraAttackOfSecondaryTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new Dictionary<DamageType, int> {
        {DamageType.COLD, 3},
        {DamageType.PHYSICAL, 2}
      }),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.True(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraAttackOfMultipleTypesWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new Dictionary<DamageType, int> {
        {DamageType.COLD, 3},
        {DamageType.PHYSICAL, 1},
        {DamageType.LIGHTNING, 1}
      }),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.True(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void LackingWildAttackDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, new Dictionary<DamageType, int> {
        {DamageType.COLD, 3}
      }),
      new Equipment(EquipmentType.SHIELD, this.enemyTwo.Attack)
    };

    Assert.False(this.enemyTwo.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraDefenseOfMainTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new Dictionary<DamageType, int>{{DamageType.FIRE, 5}})
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraDefenseOfSecondaryTypeWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new Dictionary<DamageType, int> {
        {DamageType.FIRE, 3},
        {DamageType.PHYSICAL, 2}
      })
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void ExtraDefenseOfMultipleTypesWorks() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new Dictionary<DamageType, int> {
        {DamageType.FIRE, 3},
        {DamageType.PHYSICAL, 1},
        {DamageType.LIGHTNING, 1}
      })
    };

    Assert.True(this.enemyThree.CanDefeatWith(gear));
  }

  [Test]
  public void LackingWildDefenseDoesNotWork() {
    var gear = new List<Equipment> {
      new Equipment(EquipmentType.ONE_HAND, this.enemyThree.Defense),
      new Equipment(EquipmentType.SHIELD, new Dictionary<DamageType, int> {
        {DamageType.FIRE, 3}
      })
    };

    Assert.False(this.enemyThree.CanDefeatWith(gear));
  }
}
