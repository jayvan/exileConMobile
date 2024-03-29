﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceDepot<T> where T : ReferenceData {
  private readonly Dictionary<string, T> depot = new Dictionary<string, T>();

  public static ReferenceDepot<T> Load<T>(Func<string[], T> constructor) where T : ReferenceData {
    ReferenceDepot<T> depot = new ReferenceDepot<T>();
    string[] lines = Resources.Load<TextAsset>("Data/" + typeof(T)).text.Split('\n');
    for (int i = 1; i < lines.Length; i++) {
      if (string.IsNullOrEmpty(lines[i])) {
        break;
      }

      T entry = constructor(lines[i].Split('\t'));
      depot.Add(entry);
    }

    return depot;
  }

  public void Add(T entry) {
    this.depot.Add(entry.Reference, entry);
  }

  public IEnumerable<T> All() {
    foreach (T referenceData in this.depot.Values) {
      yield return referenceData;
    }
  }

  public T Get(string reference, bool ignoreWarning = false) {
    T result;
    if (!this.depot.TryGetValue(reference, out result)) {
      if (!ignoreWarning) {
        Debug.LogError("Could not find " + typeof(T) + " with reference '" + reference + "'");
      }
    }

    return result;
  }
}
