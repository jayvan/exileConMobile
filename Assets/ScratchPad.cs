using UnityEngine;

public class ScratchPad : MonoBehaviour {
  public CardView cardView;
  public CardView cardViewTwo;
  public CardView cardViewThree;
  public CardView cardViewFour;
    void Start() {
      Data.Load();

      Equipment equip = new Equipment(Data.BaseEquipments.Get("2mace_4"));
      equip.SetRolledMod(Data.DamageSets.Get("mod_phys_2"), Rarity.Magic);
      this.cardView.SetItem(equip);

      equip = new Equipment(Data.BaseEquipments.Get("unq_aml_volls"));
      this.cardViewTwo.SetItem(equip);

      equip = new Equipment(Data.BaseEquipments.Get("ring_cold"));
      equip.SetRolledMod(Data.DamageSets.Get("mod_ring_rare_1"), Rarity.Rare);
      equip.RemoveDurability();
      equip.RemoveDurability();
      this.cardViewThree.SetItem(equip);

      equip = new Equipment(Data.BaseEquipments.Get("str_shield_1"));
      equip.RemoveDurability();
      this.cardViewFour.SetItem(equip);
    }
}
