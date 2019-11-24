using System.Collections.Generic;

public class Enemy {
  public readonly Dictionary<DamageType, int> Attack;
  public readonly Dictionary<DamageType, int> Defense;
  private readonly Clipper clipper;

  public Enemy(Dictionary<DamageType, int> attack, Dictionary<DamageType, int> defense) {
    this.Attack = attack;
    this.Defense = defense;
  }

  public bool CanDefeatWith(List<Equipment> playerEquipment) {
    var playerAttack = new Dictionary<DamageType, int>();
    var playerDefense = new Dictionary<DamageType, int>();

    // TODO: Player wilds and jewelry
    // In the first pass, weapons, shields, and body armour is calculated
    // Rings are amulets are calculated afterwards and split as needed between attack/defense
    foreach (Equipment equipment in playerEquipment) {
      switch (equipment.EquipmentType) {
        case EquipmentType.ONE_HAND:
        case EquipmentType.TWO_HAND:
          playerAttack.Add(equipment.DamageTypes);
          break;
        case EquipmentType.BODY:
        case EquipmentType.SHIELD:
          playerDefense.Add(equipment.DamageTypes);
          break;
      }
    }

    playerAttack.Subtract(this.Defense);
    playerDefense.Subtract(this.Attack);

    return DamageSatisfied(playerAttack) && DamageSatisfied(playerDefense);
  }

  public void DamageEquipment(List<Equipment> equipment) {

  }

  private bool DamageSatisfied(Dictionary<DamageType, int> damage) {
    int extra = 0;

    foreach (KeyValuePair<DamageType, int> kvp in damage) {
      if (kvp.Key == DamageType.WILD) {
        extra += kvp.Value;
        continue;
      }

      if (kvp.Value < 0) {
        return false;
      }

      extra += kvp.Value;
    }

    return extra >= 0;
  }
}
