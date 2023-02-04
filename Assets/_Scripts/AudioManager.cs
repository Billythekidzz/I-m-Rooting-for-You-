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

    [SerializeField]
    AudioSource musicSource;

    public static AudioManager Instance;

    [SerializeField]
    StringAudioElementListDictionary stringAudioClipDictionary;

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

    public AudioElement PlaySound(string audioKey)
    {
        if(stringAudioClipDictionary.TryGetValue(audioKey, out List<AudioElement> value))
        {
            int indexToUse = UnityEngine.Random.Range(0, value.Count);
            sfxSource.PlayOneShot(value[indexToUse].audioClip, value[indexToUse].volume);
            return value[indexToUse];
        }
        return null;
    }

    public AudioElement PlayMusic(string audioKey)
    {
        if(audioKey == Globals.MUSIC_STOP_KEY)
        {
            musicSource.Stop();
            return null;
        }
        if (stringAudioClipDictionary.TryGetValue(audioKey, out List<AudioElement> value))
        {
            int indexToUse = UnityEngine.Random.Range(0, value.Count);
            musicSource.clip = value[indexToUse].audioClip;
            musicSource.volume = value[indexToUse].volume;
            musicSource.Play();
            //musicSource.PlayOneShot(value[indexToUse].audioClip, value[indexToUse].volume);
            return value[indexToUse];
        }
        return null;
    }
}

[Serializable]
public class AudioElement
{
    public AudioClip audioClip;
    public float volume = 1.0f;
    public float delay = 0.5f;
}


[Serializable]
public class AudioElementListStorage : SerializableDictionary.Storage<List<AudioElement>>
{ }

[Serializable]
public class StringAudioElementListDictionary : SerializableDictionary<string,
List<AudioElement>, AudioElementListStorage>
{ }

