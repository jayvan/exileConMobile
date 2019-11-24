public class RingComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.RING  ? 0 : 1;
  }
}
