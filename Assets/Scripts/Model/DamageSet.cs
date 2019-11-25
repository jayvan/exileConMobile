using System;

public struct DamageSet {
  public int Block;
  public int Physical;
  public int Fire;
  public int Cold;
  public int Lightning;
  public int Chaos;
  public int Extra;
  public int Wild;

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

  public bool DamageSatisfied() {
    return this.Wild >= Math.Abs(Math.Min(this.Block, 0) + Math.Min(this.Physical, 0) + Math.Min(this.Fire, 0) + Math.Min(this.Cold, 0) + Math.Min(this.Lightning, 0) + Math.Min(this.Chaos, 0) + Math.Min(this.Extra, 0));
  }
}
