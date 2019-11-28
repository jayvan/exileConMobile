public class ShieldComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.Shield  ? 0 : 1;
  }
}
