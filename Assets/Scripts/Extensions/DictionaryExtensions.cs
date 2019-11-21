using System.Collections.Generic;

public static class DictionaryExtensions {
  public static void Add<T>(this Dictionary<T, int> a, Dictionary<T, int> b) {
    foreach (KeyValuePair<T, int> kvp in b) {
      if (!a.ContainsKey(kvp.Key)) {
        a.Add(kvp.Key, kvp.Value);
      } else {
        a[kvp.Key] += kvp.Value;
      }
    }
  }

  public static void Subtract<T>(this Dictionary<T, int> a, Dictionary<T, int> b) {
    foreach (KeyValuePair<T, int> kvp in b) {
      if (!a.ContainsKey(kvp.Key)) {
        a.Add(kvp.Key, -kvp.Value);
      } else {
        a[kvp.Key] -= kvp.Value;
      }
    }
  }
}
