public class ShieldComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.SHIELD  ? 0 : 1;
  }
}
