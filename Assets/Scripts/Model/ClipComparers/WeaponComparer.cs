public class WeaponComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.OneHand || equipment.EquipmentType == EquipmentType.TwoHand
      ? 0
      : 1;
  }
}
