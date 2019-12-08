using UnityEngine;
using UnityEngine.UI;

class DamageSetView : MonoBehaviour {
  [SerializeField] private GameObject iconPrefab;
  [SerializeField] private DamageIcons icons;

  public void SetDamage(DamageSet damageSet) {
    ClearDamage();

    this.Add(DamageType.BLOCK, damageSet.Block);
    this.Add(DamageType.PHYSICAL, damageSet.Physical);
    this.Add(DamageType.FIRE, damageSet.Fire);
    this.Add(DamageType.COLD, damageSet.Cold);
    this.Add(DamageType.LIGHTNING, damageSet.Lightning);
    this.Add(DamageType.CHAOS, damageSet.Chaos);
    this.Add(DamageType.WILD, damageSet.Wild);
    this.Add(DamageType.LIFE, damageSet.Extra);
  }

  public void ClearDamage() {
    foreach (GameObject g in this.transform) {
      Destroy(g);
    }
  }

  private void Add(DamageType type, int quantity) {
    for (int i = 0; i < quantity; i++) {
      Instantiate(this.iconPrefab, this.transform, false).GetComponent<Image>().sprite = this.icons.Get(type);
    }
  }
}
