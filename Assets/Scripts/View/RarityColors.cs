using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RarityColors", order = 1)]
public class RarityColors : ScriptableObject {
  public Color Normal;
  public Color Magic;
  public Color Rare;
  public Color Unique;
  public Color Currency;

  public Color Get(Equipment equipment) {
    switch (equipment.Rarity) {
      case Rarity.Normal:
        return this.Normal;
      case Rarity.Magic:
        return this.Magic;
      case Rarity.Rare:
        return this.Rare;
      case Rarity.Unique:
        return this.Unique;
      default:
        throw new ArgumentOutOfRangeException(nameof(equipment.Rarity), equipment.Rarity, null);
    }
  }
}
