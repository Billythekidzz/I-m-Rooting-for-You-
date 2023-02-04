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

    public Dictionary<string, int> characterAffections = new Dictionary<string, int>();

    [SerializeField]
    GameObject optionsPanel;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionsPanel();
        }
    }

    public void ToggleOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
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
            if (characterKeyToSet == Globals.MC_KEY)
            {
                wasLastSpeakerMC = true;
            }
            else
            {
                wasLastSpeakerMC = false;
            }
        }
    }

    public void AddAffinity(string param)
    {
        string characterKey = param.Split('|')[0].ToLower();
        string floatIntString = param.Split('|')[1];
        if (int.TryParse(floatIntString, out int value))
        {
            if(characterAffections.TryGetValue(characterKey, out int currentAffection))
            {
                characterAffections[characterKey] = currentAffection + value;
            }
            else
            {
                characterAffections.Add(characterKey, value);
            }
            Debug.Log("Added " + value + " to " + characterKey);
        }
    }

    public void SaveGameState()
    {
        //tbd, for saving to local file with easysave
    }

    public bool IsOptionsOpen()
    {
        return optionsPanel.activeSelf;
    }
}
