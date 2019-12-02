using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageIcons", order = 1)]
public class DamageIcons : ScriptableObject {
  public Sprite[] Sprites;

  public Sprite Get(DamageType damageType) {
    return this.Sprites[(int) damageType];
  }
}
