public static class PlayerInventory {
  private const int MAXIMUM_ITEMS = 1000;
  private static IntPlayerData lastItemSlot = new IntPlayerData("last_item_slot");

  public static void Load() {

  }

  public static Equipment FromSave(SavedEquipment saved) {
    BaseEquipment baseEquipment = Data.BaseEquipments.Get(saved.BaseReference);
    DamageSetConfig rolledMod = null;
    if (!string.IsNullOrEmpty(saved.RolledModReference)) {
      rolledMod = Data.DamageSets.Get(saved.RolledModReference);
    }
    return new Equipment(baseEquipment, saved.Rarity, rolledMod, saved.Damage, saved.HasQuality);
  }
}
