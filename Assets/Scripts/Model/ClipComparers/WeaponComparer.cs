public class WeaponComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.ONE_HAND || equipment.EquipmentType == EquipmentType.TWO_HAND
      ? 0
      : 1;
  }
}
