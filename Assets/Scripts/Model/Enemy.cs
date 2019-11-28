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
    var playerJewelry = new DamageSet();

    // TODO: Player wilds and jewelry
    // In the first pass, weapons, shields, and body armour is calculated
    // Rings are amulets are calculated afterwards and split as needed between attack/defense
    foreach (Equipment equipment in playerEquipment) {
      switch (equipment.EquipmentType) {
        case EquipmentType.OneHand:
        case EquipmentType.TwoHand:
          playerAttack += equipment.DamageTypes;
          break;
        case EquipmentType.Body:
        case EquipmentType.Shield:
          playerDefense += equipment.DamageTypes;
          break;
        case EquipmentType.Ring:
        case EquipmentType.Amulet:
          playerAttack.Physical += equipment.DamageTypes.Physical;
          playerDefense.Block += equipment.DamageTypes.Block;
          playerJewelry += equipment.DamageTypes;
          break;
      }
    }

    // These have been moved to attack and defense
    playerJewelry.Physical = 0;
    playerJewelry.Block = 0;

    playerAttack = (playerAttack - this.Defense).ConvertToExtra();
    playerDefense = (playerDefense - this.Attack).ConvertToExtra();

    // If player attack isn't strong enough, try to borrow stats from Jewelry
    if (!playerAttack.DamageSatisfied()) {
      int attackDeficit = playerAttack.DamageDeficit;

      foreach (DamageSet jewelryAllocation in playerJewelry.Subsets(attackDeficit)) {
        if ((playerAttack + jewelryAllocation).ConvertToExtra().DamageSatisfied() &&
            (playerDefense + playerJewelry - jewelryAllocation).ConvertToExtra().DamageSatisfied()) {
          return true;
        }
      }
    }

    return playerAttack.DamageSatisfied() && (playerDefense + playerJewelry).ConvertToExtra().DamageSatisfied();
  }
}
