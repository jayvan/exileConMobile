using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class CardView : MonoBehaviour {
  [SerializeField] private Image backgroundImage;
  [SerializeField] private Text itemName;
  [SerializeField] private DamageSetView implicitMods;
  [SerializeField] private Text currencyDescription;
  [SerializeField] private Text modifierName;
  [SerializeField] private DamageSetView explicitMods;
  [SerializeField] private GameObject[] equipmentTypes;
  [SerializeField] private GameObject[] durabilities;
  [SerializeField] private Image itemImage;
  [SerializeField] private RarityColors rarityColors;

  public void SetItem(CurrencyType currencyType) {
    this.itemName.text = currencyType.Name();
    this.itemName.color = this.rarityColors.Rare;
    this.currencyDescription.text = currencyType.Description();
    this.modifierName.text = string.Empty;
    this.implicitMods.ClearDamage();
    this.explicitMods.ClearDamage();
    this.SetActiveType(null);
    this.SetDamage(0);

    Addressables.LoadAssetAsync<Sprite>("card_base/Currency").Completed += load => {
      this.backgroundImage.sprite = load.Result;
    };

    Addressables.LoadAssetAsync<Sprite>("currency/" + currencyType).Completed += load => {
      this.itemImage.sprite = load.Result;
    };
  }

  public void SetItem(Equipment item) {
    Addressables.LoadAssetAsync<Sprite>("card_base/" + item.Rarity).Completed += load => {
      this.backgroundImage.sprite = load.Result;
    };

    Addressables.LoadAssetAsync<Sprite>("items/" + item.Base.Reference).Completed += load => {
      this.itemImage.sprite = load.Result;
    };

    string modifierText = string.Empty;

    if (item.Rarity != Rarity.Unique) {
      modifierText = item.ModifierName ?? Data.Translations.Get(item.Base.EquipmentType.ToString()).Value;
    }

    this.modifierName.text = modifierText;
    this.currencyDescription.text = string.Empty;
    this.itemName.text = item.Name;
    this.itemName.color = this.modifierName.color = this.rarityColors.Get(item);
    this.SetActiveType(item.EquipmentType);
    this.SetDamage(item.Damage);
    this.implicitMods.SetDamage(item.BaseDamageTypes);
    this.explicitMods.SetDamage(item.RolledDamageTypes);
  }

  private void SetDamage(int damage) {
    for (int i = 0; i < this.durabilities.Length; i++) {
      this.durabilities[i].SetActive(damage > i);
    }
  }

  private void SetActiveType(EquipmentType? equipmentType) {
    for (int i = 0; i < this.equipmentTypes.Length; i++) {
      this.equipmentTypes[i].SetActive(equipmentType.HasValue && (int)equipmentType.Value == i);
    }
  }
}
