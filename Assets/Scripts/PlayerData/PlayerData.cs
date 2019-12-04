using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class PlayerData<T> {
  private static BinaryFormatter formatter = new BinaryFormatter();
  private static MemoryStream stream = new MemoryStream(1000);
  private readonly string Reference;
  private T value;

  public T Value {
    get {
      return this.value;
    }

    set {
      this.value = value;
      stream.SetLength(0);
      stream.Seek(0, SeekOrigin.Begin);
      formatter.Serialize(stream, value);
      string serializedString = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
      Debug.Log(serializedString);
      PlayerPrefs.SetString(this.Reference, serializedString);
      PlayerPrefs.Save();
    }
  }

  public PlayerData(string reference) {
    this.Reference = reference;
    stream.SetLength(0);

    string encoded = PlayerPrefs.GetString(reference, string.Empty);
    byte[] encodedBytes = Encoding.UTF8.GetBytes(encoded);
    stream.Write(encodedBytes, 0, encodedBytes.Length);

    if (stream.Length > 0) {
      this.value = (T)formatter.Deserialize(stream);
    }
  }
}
