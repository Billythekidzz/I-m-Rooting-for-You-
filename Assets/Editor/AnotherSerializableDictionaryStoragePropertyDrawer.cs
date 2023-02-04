using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(AudioElementListStorage))]
public class AnotherSerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

[CustomPropertyDrawer(typeof(StringAudioElementListDictionary))]
[CustomPropertyDrawer(typeof(StringImageElementListDictionary))]
[CustomPropertyDrawer(typeof(Minigames.MinigameEntry))]
public class AnotherSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

