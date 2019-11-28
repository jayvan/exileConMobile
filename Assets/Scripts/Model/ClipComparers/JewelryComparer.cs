public class JewelryComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.Amulet || equipment.EquipmentType == EquipmentType.Ring
      ? 0
      : 1;
  }
}
