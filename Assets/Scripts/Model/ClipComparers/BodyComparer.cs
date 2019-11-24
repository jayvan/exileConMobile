public class BodyComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return equipment.EquipmentType == EquipmentType.BODY ? 0 : 1;
  }
}
