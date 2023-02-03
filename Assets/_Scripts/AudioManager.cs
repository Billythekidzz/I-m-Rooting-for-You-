using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    [SerializeField]
    AudioSource sfxSource;

    public static AudioManager Instance;

    [SerializeField]
    StringAudioClipListDictionary stringAudioClipDictionary;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlaySound(string audioKey)
    {
        if(stringAudioClipDictionary.TryGetValue(audioKey, out List<AudioClip> value))
        {
            //Just play the first for now
            sfxSource.PlayOneShot(value[0]);
        }
    }
}

[Serializable]
public class AudioClipListStorage : SerializableDictionary.Storage<List<AudioClip>>
{ }

[Serializable]
public class StringAudioClipListDictionary : SerializableDictionary<string,
List<AudioClip>, AudioClipListStorage>
{ }