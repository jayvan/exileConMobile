public class MostDamagedComparer : ClipComparer {
  protected override int EquipmentValue(Equipment equipment) {
    return -equipment.Damage;
  }
}
