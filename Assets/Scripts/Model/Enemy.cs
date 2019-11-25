using System.Collections.Generic;

public class Enemy {
  public readonly DamageSet Attack;
  public readonly DamageSet Defense;
  private readonly Clipper clipper;

  public Enemy(DamageSet attack, DamageSet defense) {
    this.Attack = attack;
    this.Defense = defense;
  }

  public bool CanDefeatWith(List<Equipment> playerEquipment) {
    var playerAttack = new DamageSet();
    var playerDefense = new DamageSet();

    // TODO: Player wilds and jewelry
    // In the first pass, weapons, shields, and body armour is calculated
    // Rings are amulets are calculated afterwards and split as needed between attack/defense
    foreach (Equipment equipment in playerEquipment) {
      switch (equipment.EquipmentType) {
        case EquipmentType.ONE_HAND:
        case EquipmentType.TWO_HAND:
          playerAttack += equipment.DamageTypes;
          break;
        case EquipmentType.BODY:
        case EquipmentType.SHIELD:
          playerDefense += equipment.DamageTypes;
          break;
      }
    }

    playerAttack = (playerAttack - this.Defense).ConvertToExtra();
    playerDefense = (playerDefense - this.Attack).ConvertToExtra();

    return playerAttack.DamageSatisfied() && playerDefense.DamageSatisfied();
  }
}
