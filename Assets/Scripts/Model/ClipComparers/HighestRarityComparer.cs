public class HighestRarityComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return -(int) equipment.Rarity;
  }
}
