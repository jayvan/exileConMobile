using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScratchPad : MonoBehaviour {
  public CardView currencyCard;
  public CardView[] cardViews;
  public CardView[] currencyCardViews;
  public GameObject currencyPrefab;
  public GameObject currencyContainer;
  private CurrencyType activeCurrency;

    void Start() {
      Data.Load();
      PlayerInventory.Load();
      PlayerInventory.Clear();
      StartCoroutine(this.RunScratchpad());
    }

    IEnumerator RunScratchpad() {
      foreach (CurrencyType value in Enum.GetValues(typeof(CurrencyType))) {
        var currency = GameObject.Instantiate(this.currencyPrefab, this.currencyContainer.transform);
        currency.GetComponent<Button>().onClick.AddListener(() => this.SetActiveCurrency(value));
          Addressables.LoadAssetAsync<Sprite>("currency/" + value).Completed += load => {
            currency.GetComponent<Image>().sprite = load.Result;
          };
      }
      SetActiveCurrency(CurrencyType.Whetstone);
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
        this.cardViews[index].SetAction(this.UseCurrency(equipment, this.cardViews[index]));
        this.cardViews[index++].SetItem(equipment);
      }

      index = 0;
      foreach (CurrencyType value in Enum.GetValues(typeof(CurrencyType))) {
        this.currencyCardViews[index++].SetItem(value);
      }

      yield return null;
    }

    public void Update() {
      for (int i = 0; i < 7; i++) {
        KeyCode code = KeyCode.Alpha0 + i;
        if (Input.GetKeyDown(code)) {
          SetActiveCurrency((CurrencyType)i);
        }
      }
    }

    private UnityAction UseCurrency(Equipment equipment, CardView cardView) {
      return () => {
        equipment.UseCurrency(this.activeCurrency);
        cardView.SetItem(equipment);
      };
    }

    private void SetActiveCurrency(CurrencyType currency) {
      this.activeCurrency = currency;
      this.currencyCard.SetItem(currency);
    }
}
