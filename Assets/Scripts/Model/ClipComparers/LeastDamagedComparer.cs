public class LeastDamagedComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return -equipment.Durability;
  }
}
