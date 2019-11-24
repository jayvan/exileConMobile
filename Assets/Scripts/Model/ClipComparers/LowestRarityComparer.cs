public class LowestRarityComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return (int) equipment.Rarity;
  }
}
