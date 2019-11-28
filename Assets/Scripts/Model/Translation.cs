public class Translation : ReferenceData {
  public readonly string Value;

  public Translation(string[] values) {
    this.Reference = values[0];
    this.Value = values[1];
  }
}
