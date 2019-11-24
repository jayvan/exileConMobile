public class JewelryComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.AMULET || equipment.EquipmentType == EquipmentType.RING
      ? 0
      : 1;
  }
}
