using System;
using System.Collections.Generic;

public struct DamageSet {
  public int Block;
  public int Physical;
  public int Fire;
  public int Cold;
  public int Lightning;
  public int Chaos;
  public int Extra;
  public int Wild;

  private const int SIZE = 8;

  public int this[int i] {
    get {
      switch (i) {
        case 0: return this.Block;
        case 1: return this.Physical;
        case 2: return this.Fire;
        case 3: return this.Cold;
        case 4: return this.Lightning;
        case 5: return this.Chaos;
        case 6: return this.Extra;
        case 7: return this.Wild;
        default: throw new IndexOutOfRangeException();
      }
    }
    set {
      switch (i) {
        case 0:
          this.Block = value;
          break;
        case 1:
          this.Physical = value;
          break;
        case 2:
          this.Fire = value;
          break;
        case 3:
          this.Cold = value;
          break;
        case 4:
          this.Lightning = value;
          break;
        case 5:
          this.Chaos = value;
          break;
        case 6:
          this.Extra = value;
          break;
        case 7:
          this.Wild = value;
          break;
        default:
          throw new IndexOutOfRangeException();
      }

    }
  }

  public static DamageSet operator +(DamageSet a, DamageSet b) {
    return new DamageSet {
      Block = a.Block + b.Block,
      Physical = a.Physical + b.Physical,
      Fire = a.Fire + b.Fire,
      Cold = a.Cold + b.Cold,
      Lightning = a.Lightning + b.Lightning,
      Chaos = a.Chaos + b.Chaos,
      Extra = a.Extra + b.Extra,
      Wild = a.Wild + b.Wild
    };
  }

  public static DamageSet operator -(DamageSet a, DamageSet b) {
    return new DamageSet {
      Block = a.Block + b.Block,
      Physical = a.Physical - b.Physical,
      Fire = a.Fire - b.Fire,
      Cold = a.Cold - b.Cold,
      Lightning = a.Lightning - b.Lightning,
      Chaos = a.Chaos - b.Chaos,
      Extra = a.Extra - b.Extra,
      Wild = a.Wild - b.Wild
    };
  }

  public DamageSet(string[] fields) {
    this.Block = int.Parse(fields[1]);
    this.Physical = int.Parse(fields[2]);
    this.Fire = int.Parse(fields[3]);
    this.Cold = int.Parse(fields[4]);
    this.Lightning = int.Parse(fields[5]);
    this.Chaos = int.Parse(fields[6]);
    this.Wild = int.Parse(fields[7]);
    this.Extra = int.Parse(fields[8]);
  }

  public DamageSet ConvertToExtra() {
    return new DamageSet {
      Block = Math.Min(0, this.Block),
      Physical = Math.Min(0, this.Physical),
      Fire = Math.Min(0, this.Fire),
      Cold = Math.Min(0, this.Cold),
      Lightning = Math.Min(0, this.Lightning),
      Chaos = Math.Min(0, this.Chaos),
      Extra = Math.Max(this.Physical, 0) + Math.Max(this.Fire, 0) + Math.Max(this.Cold, 0) +
              Math.Max(this.Lightning, 0) + Math.Max(this.Chaos, 0) + Math.Max(this.Block, 0) + this.Extra,
      Wild = this.Wild
    };
  }

  public int DamageDeficit => Math.Abs(Math.Min(this.Block, 0) + Math.Min(this.Physical, 0) + Math.Min(this.Fire, 0) + Math.Min(this.Cold, 0) + Math.Min(this.Lightning, 0) + Math.Min(this.Chaos, 0) + Math.Min(this.Extra, 0)) - this.Wild;

  public bool AnyNegative {
    get {
      for (int i = 0; i < SIZE; i++) {
        if (this[i] < 0) {
          return true;
        }
      }

      return false;
    }
  }

  public bool DamageSatisfied() {
    return this.DamageDeficit <= 0;
  }

  public IEnumerable<DamageSet> Subsets(int size) {
    // Build initial state
    DamageSet tmp = new DamageSet();
    for (int i = 0; i < SIZE && size > 0; i++) {
      int sizeAtIndex = Math.Min(size, this[i]);
      tmp[i] = sizeAtIndex;
      size -= sizeAtIndex;
    }

    int rightmost, next;

    if (size == 0) {
      yield return tmp;
    } else {
      yield break;
    }

    do {
      rightmost = tmp.RightmostNonZero();
      next = this.NextNonZero(rightmost);
      if (next != -1) {
        tmp[rightmost]--;
        tmp[next]++;
      } else {
        int falloff = tmp[rightmost];
        tmp[rightmost] = 0;
        rightmost = tmp.RightmostNonZero();
        if (rightmost <= 0) {
          break;
        }
        tmp[rightmost++]--;
        tmp[rightmost] = falloff + 1;
      }

      if (!(this-tmp).AnyNegative) {
        yield return tmp;
      }
    } while (true);
  }

  private int NextNonZero(int i) {
    for (i++; i < SIZE; i++) {
      if (this[i] > 0) {
        return i;
      }
    }

    return -1;
  }

  private int RightmostNonZero() {
    for (int i = SIZE - 1; i >= 0; i--) {
      if (this[i] > 0) {
        return i;
      }
    }

    return 0;
  }

  public override string ToString() {
    return
      $"[{this.Block}, {this.Physical}, {this.Fire}, {this.Cold}, {this.Lightning}, {this.Chaos}, {this.Extra}, {this.Wild}]";
  }
}
