using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AudioClipListStorage))]
public class AnotherSerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

[CustomPropertyDrawer(typeof(StringAudioClipListDictionary))]
public class AnotherSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }