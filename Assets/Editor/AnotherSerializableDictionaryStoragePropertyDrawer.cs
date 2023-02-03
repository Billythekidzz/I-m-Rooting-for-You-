using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AudioClipListStorage))]
public class AnotherSerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

[CustomPropertyDrawer(typeof(StringAudioClipListDictionary))]
[CustomPropertyDrawer(typeof(StringTexture2DDictionary))]
public class AnotherSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }