using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AudioElementListStorage))]
public class AnotherSerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

[CustomPropertyDrawer(typeof(StringAudioElementListDictionary))]
[CustomPropertyDrawer(typeof(StringTexture2DDictionary))]
public class AnotherSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }