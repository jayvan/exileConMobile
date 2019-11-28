using System;
using System.Collections.Generic;

public class Clipper {
  private ClipStrategy strategy;
  private ClipComparer comparer;
  private int clipCount;

  public Clipper(ClipStrategy strategy, int count, ClipPriority[] priorities = null) {
    this.strategy = strategy;
    this.clipCount = count;

    if (this.strategy != ClipStrategy.CLIP_ALL) {
      this.comparer = GetComparer(priorities[0]);

      ClipComparer current = this.comparer;
      for (int i = 1; i < priorities.Length; i++) {
        current = current.SetNext(GetComparer(priorities[i]));
      }
    }
  }

  public Dictionary<Equipment, int> Clip(List<Equipment> equipment) {
    var result = new Dictionary<Equipment, int>();

    if (this.strategy == ClipStrategy.CLIP_ALL) {
      foreach (Equipment equip in equipment) {
        result.Add(equip, Math.Min(this.clipCount, equip.Damage));
      }
    } else {
      equipment.Sort(this.comparer);
      int equipmentIndex = 0;

      for (int i = 0; i < this.clipCount; i++) {
        Equipment equip = equipment[equipmentIndex];

        if (!result.ContainsKey(equip)) {
          result.Add(equip, 1);
        } else {
          result[equip]++;
        }

        if (this.strategy == ClipStrategy.CLIP_DISTINCT || equip.Damage - result[equip] <= 0) {
          equipmentIndex++;
        }

        if (equipmentIndex >= equipment.Count) {
          break;
        }
      }
    }

    return result;
  }

  private ClipComparer GetComparer(ClipPriority priority) {
    switch (priority) {
      case ClipPriority.MOST_DAMAGED:
        return new MostDamagedComparer();
      case ClipPriority.LEAST_DAMAGED:
        return new LeastDamagedComparer();
      case ClipPriority.MOST_RARE:
        return new HighestRarityComparer();
      case ClipPriority.LEAST_RARE:
        return new LowestRarityComparer();
      case ClipPriority.WEAPON:
        return new WeaponComparer();
      case ClipPriority.SHIELD:
        return new ShieldComparer();
      case ClipPriority.BODY:
        return new BodyComparer();
      case ClipPriority.AMULET:
        return new AmuletComparer();
      case ClipPriority.RING:
        return new RingComparer();
      case ClipPriority.JEWELRY:
        return new JewelryComparer();
      default:
        throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
    }
  }
}
