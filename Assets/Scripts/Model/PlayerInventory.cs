using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public static class PlayerInventory {
  private const int MAXIMUM_ITEMS = 1000;
  private const string ITEM_PREFIX = "item_";
  private const string CURRENCY_PREFIX = "currency_";
  private static IntPlayerData lastItemSlot = new IntPlayerData("last_item_slot");
  private static PlayerData<SavedEquipment>[] savedItems = new PlayerData<SavedEquipment>[MAXIMUM_ITEMS];
  private static Equipment[] equipment = new Equipment[MAXIMUM_ITEMS];
  private static Dictionary<CurrencyType, IntPlayerData> currencies = new Dictionary<CurrencyType, IntPlayerData>();

  public static void Load() {
    for (int i = 0; i < savedItems.Length; i++) {
      savedItems[i] = new PlayerData<SavedEquipment>(ITEM_PREFIX + i);
      if (savedItems[i].Value != null) {
        equipment[i] = FromSave(savedItems[i].Value);
      }
    }

    foreach (CurrencyType currency in Enum.GetValues(typeof(CurrencyType))) {
      currencies.Add(currency, new IntPlayerData(CURRENCY_PREFIX + currency));
    }
  }

  public static void Clear() {
    for (int i = 0; i < savedItems.Length; i++) {
      savedItems[i].Value = null;
      equipment[i] = null;
    }

    foreach (KeyValuePair<CurrencyType, IntPlayerData> kvp in currencies) {
      kvp.Value.Value = 0;
    }

    lastItemSlot.Value = 0;
  }

  public static void Grant(Equipment equip) {
    equipment[lastItemSlot.Value] = equip;
    savedItems[lastItemSlot.Value].Value = equip.SavedEquipment;
    lastItemSlot.Value++;
  }

  public static void Grant(CurrencyType type, int quantity = 1) {
    currencies[type].Value += quantity;
  }

  public static bool Consume(CurrencyType type, int quantity = 1) {
    if (currencies[type].Value < quantity) {
      return false;
    }

    currencies[type].Value -= quantity;
    return true;
  }

  public static void Destroy(Equipment equip) {
    int index = Array.IndexOf(equipment, equip);

    if (index < 0) {
      return;
    }

    if (index != lastItemSlot.Value - 1) {
      equipment[index] = equipment[lastItemSlot.Value - 1];
      savedItems[index].Value = equipment[index].SavedEquipment;
      equipment[lastItemSlot.Value - 1] = null;
      savedItems[lastItemSlot.Value - 1].Value = null;
    } else {
      equipment[index] = null;
      savedItems[index].Value = null;
    }

    lastItemSlot.Value--;
  }

  public static IEnumerable<Equipment> All() {
    for (int i = 0; i < lastItemSlot.Value; i++) {
      yield return equipment[i];
    }
  }

  public static IEnumerable<Equipment> OfType(EquipmentType type) {
    return ReturnFiltered(equip => equip.EquipmentType == type);
  }

  private static IEnumerable<Equipment> ReturnFiltered(Func<Equipment, bool> filter) {
    for (int i = 0; i < lastItemSlot.Value; i++) {
      if (filter(equipment[i])) {
        yield return equipment[i];
      }
    }
  }

  private static Equipment FromSave(SavedEquipment saved) {
    BaseEquipment baseEquipment = Data.BaseEquipments.Get(saved.BaseReference);
    DamageSetConfig rolledMod = null;
    if (!string.IsNullOrEmpty(saved.RolledModReference)) {
      rolledMod = Data.DamageSets.Get(saved.RolledModReference);
    }
    return new Equipment(baseEquipment, saved.Rarity, rolledMod, saved.Damage, saved.HasQuality);
  }
}
