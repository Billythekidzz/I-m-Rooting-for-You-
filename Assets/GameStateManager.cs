using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{

    public string lastSavedCharacterKey = "";

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
        if(lastSavedCharacterKey != characterKeyToSet)
        {
            lastSavedCharacterKey = characterKeyToSet;
            characterChangedEvent.Invoke();
        }
    }
}
