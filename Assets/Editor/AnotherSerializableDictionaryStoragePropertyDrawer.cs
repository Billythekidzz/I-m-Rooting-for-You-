using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AudioElementListStorage))]
public class AnotherSerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

[CustomPropertyDrawer(typeof(StringAudioElementListDictionary))]
[CustomPropertyDrawer(typeof(StringImageElementListDictionary))]
public class AnotherSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }