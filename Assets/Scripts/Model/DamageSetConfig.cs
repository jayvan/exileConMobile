using System;

public class DamageSetConfig : ReferenceData {
  public readonly DamageSet DamageSet;
  public readonly string Translation;

  public DamageSetConfig(string[] fields, Translation translation) {
    this.Reference = fields[0];
    this.DamageSet = new DamageSet(fields);
    this.Translation = translation == null ? string.Empty : translation.Value;
  }
}
