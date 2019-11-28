public class AmuletComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.Amulet  ? 0 : 1;
  }
}
