using System;
using System.Collections.Generic;

public static class ListExtensions {
  private static Random internalRandom = new Random(Environment.TickCount);
  public static void Shuffle<T>(this List<T> list, Random random = null) {
    // Fisher-Yates Shuffle
    Random rand = random ?? internalRandom;

    for (int i = list.Count - 1; i > 0; i--) {
      int j = rand.Next(i + 1);
      var temp = list[i];
      list[i] = list[j];
      list[j] = temp;
    }
  }
}
