using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class ScratchPad : MonoBehaviour {
  public CardView cardView;
  public CardView cardViewTwo;
  public CardView cardViewThree;
  public CardView cardViewFour;
    void Start() {
      Data.Load();
      StartCoroutine(this.RunScratchpad());
    }

    IEnumerator RunScratchpad() {
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

      var formatter = new BinaryFormatter();
      MemoryStream stream = new MemoryStream();
      formatter.Serialize(stream, equip.SavedEquipment);
      stream.Seek(0, SeekOrigin.Begin);
      string serializedString = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
      PlayerPrefs.SetString("test", serializedString);
      stream.SetLength(0);
      string str = PlayerPrefs.GetString("test");
      var bytes = Encoding.UTF8.GetBytes(str);
      stream.Write(bytes, 0, bytes.Length);
      stream.Seek(0, SeekOrigin.Begin);
      equip = PlayerInventory.FromSave(formatter.Deserialize(stream) as SavedEquipment);
      this.cardViewFour.SetItem(equip);

      yield break;

      var saved = new PlayerData<SavedEquipment>("foo");
      saved.Value = equip.SavedEquipment;

      yield return new WaitForSeconds(0.01f);

      var restored = new PlayerData<SavedEquipment>("foo");
      equip = PlayerInventory.FromSave(restored.Value);
      this.cardViewFour.SetItem(equip);
    }
}
