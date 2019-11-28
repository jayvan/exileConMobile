public class DamageSetConfig : ReferenceData {
  public readonly DamageSet DamageSet;

  public DamageSetConfig(string[] fields) {
    this.Reference = fields[0];
    this.DamageSet = new DamageSet(fields);
  }
}
