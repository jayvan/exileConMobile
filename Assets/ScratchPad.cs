using UnityEngine;

public class ScratchPad : MonoBehaviour {
  public CardView cardView;
    void Start() {
      Data.Load();

      Equipment equip = new Equipment(Data.BaseEquipments.Get("1mace_2"));
      this.cardView.SetItem(equip);
    }
}
