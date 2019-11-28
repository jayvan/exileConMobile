public class RingComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.Ring  ? 0 : 1;
  }
}
