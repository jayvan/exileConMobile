using System.IO;
using System.Xml.Serialization;
using System.Text;
using UnityEngine;

public class PlayerData<T> {
  private static MemoryStream stream = new MemoryStream(1000);
  private readonly string Reference;
  private readonly XmlSerializer formatter = new XmlSerializer(typeof(T));
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
      PlayerPrefs.SetString(this.Reference, serializedString);
    }
  }

  public PlayerData(string reference) {
    this.Reference = reference;
    stream.SetLength(0);

    string encoded = PlayerPrefs.GetString(reference, string.Empty);
    byte[] encodedBytes = Encoding.UTF8.GetBytes(encoded);
    stream.Write(encodedBytes, 0, encodedBytes.Length);
    stream.Seek(0, SeekOrigin.Begin);

    if (stream.Length > 0) {
      this.value = (T)formatter.Deserialize(stream);
    }
  }
}
