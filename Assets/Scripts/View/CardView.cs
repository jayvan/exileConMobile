using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class CardView : MonoBehaviour {
  [SerializeField] private Image backgroundImage;
  [SerializeField] private Text itemName;
  [SerializeField] private DamageSetView implicitMods;
  [SerializeField] private Text modifierName;
  [SerializeField] private DamageSetView explicitMods;
  [SerializeField] private GameObject[] equipmentTypes;
  [SerializeField] private GameObject[] durabilities;
  [SerializeField] private Image itemImage;
  [SerializeField] private RarityColors rarityColors;

  public void SetItem(Equipment item) {
    // TODO: Font Colors
    // TODO: Image
    Addressables.LoadAssetAsync<Sprite>("card_base/" + item.Rarity).Completed += load => {
      this.backgroundImage.sprite = load.Result;
    };

    this.itemName.text = item.Name;
    this.modifierName.text = item.ModifierName;

    this.itemName.color = this.modifierName.color = this.rarityColors.Get(item);

    for (int i = 0; i < this.equipmentTypes.Length; i++) {
      this.equipmentTypes[i].SetActive((int)item.EquipmentType == i);
    }

    for (int i = 0; i < this.durabilities.Length; i++) {
      this.durabilities[i].SetActive(item.Damage > i);
    }

    this.implicitMods.SetDamage(item.BaseDamageTypes);
    this.explicitMods.SetDamage(item.RolledDamageTypes);
  }
}
