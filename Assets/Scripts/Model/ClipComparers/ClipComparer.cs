using System.Collections.Generic;

public abstract class ClipComparer : IComparer<Equipment> {
  private ClipComparer nextComparer;

  public ClipComparer SetNext(ClipComparer nextComparer) {
    this.nextComparer = nextComparer;
    return nextComparer;
  }

  public int Compare(Equipment x, Equipment y) {
    int xValue = this.EquipmentValue(x);
    int yValue = this.EquipmentValue(y);

    int comparison = xValue.CompareTo(yValue);

    if (comparison == 0 && this.nextComparer != null) {
      return this.nextComparer.Compare(x, y);
    }

    return comparison;
  }

  protected abstract int EquipmentValue(Equipment equipment);
}
