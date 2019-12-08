using System;
using System.Collections;
using UnityEngine;

public class ScratchPad : MonoBehaviour {
  public CardView[] cardViews;
  public CardView[] currencyCardViews;
    void Start() {
      Data.Load();
      PlayerInventory.Load();
      PlayerInventory.Clear();
      StartCoroutine(this.RunScratchpad());
    }

    IEnumerator RunScratchpad() {
      Equipment equip = new Equipment(Data.BaseEquipments.Get("2mace_4"));
      equip.SetRolledMod(Data.DamageSets.Get("mod_phys_2"), Rarity.Magic);
      PlayerInventory.Grant(equip);

      equip = new Equipment(Data.BaseEquipments.Get("unq_aml_volls"));
      PlayerInventory.Grant(equip);

      equip = new Equipment(Data.BaseEquipments.Get("ring_cold"));
      equip.SetRolledMod(Data.DamageSets.Get("mod_ring_rare_1"), Rarity.Rare);
      equip.RemoveDurability();
      equip.RemoveDurability();
      PlayerInventory.Grant(equip);

      equip = new Equipment(Data.BaseEquipments.Get("2mace_4"));
      PlayerInventory.Grant(equip);
      PlayerInventory.Destroy(equip);

      equip = new Equipment(Data.BaseEquipments.Get("str_shield_1"));
      equip.RemoveDurability();
      PlayerInventory.Grant(equip);

      int index = 0;
      foreach (Equipment equipment in PlayerInventory.All()) {
        this.cardViews[index++].SetItem(equipment);
      }

      index = 0;
      foreach (CurrencyType value in Enum.GetValues(typeof(CurrencyType))) {
        this.currencyCardViews[index++].SetItem(value);
      }

      yield return null;
    }
}
