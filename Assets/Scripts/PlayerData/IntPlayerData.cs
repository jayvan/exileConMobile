using UnityEngine;

public class IntPlayerData {
  private readonly string Reference;
  private int value;

  public int Value {
    get {
      return this.value;
    }

    set {
      this.value = value;
      PlayerPrefs.SetInt(this.Reference, value);
    }
  }

  public IntPlayerData(string reference) {
    this.Reference = reference;
    this.value = PlayerPrefs.GetInt(reference);
  }
}
