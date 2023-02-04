using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{

    [HideInInspector]
    public string lastSavedCharacterKey = "";

    [HideInInspector]
    public bool isDialogueSkippable = true;

    public bool wasLastSpeakerMC = true;

    public static GameStateManager Instance;

    public UnityEvent characterChangedEvent = new UnityEvent();

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

    public void SetCharacter(string characterKeyToSet)
    {
        if (lastSavedCharacterKey != characterKeyToSet && characterKeyToSet != Globals.MC_KEY)
        {
            wasLastSpeakerMC = false;
            lastSavedCharacterKey = characterKeyToSet;
            characterChangedEvent.Invoke();
        }
        else
        {
            //TBD FIX
            wasLastSpeakerMC = true;
        }
    }
}
