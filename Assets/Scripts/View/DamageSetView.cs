using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

class DamageSetView : MonoBehaviour {
  [SerializeField] private GameObject container;
  [SerializeField] private Dictionary<DamageType, string> mapping;

  public void SetDamage(DamageSet itemBaseDamageTypes) {
    // Clear container and instantiate damage types
  }
}
