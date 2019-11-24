public class AmuletComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.AMULET  ? 0 : 1;
  }
}
