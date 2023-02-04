using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{

    [HideInInspector]
    public string lastSavedCharacterKey = "";

    [HideInInspector]
    public string currentPath = "";

    [HideInInspector]
    public bool isDialogueSkippable = true;

    public bool wasLastSpeakerMC = true;

    public static GameStateManager Instance;

    public UnityEvent characterChangedEvent = new UnityEvent();

    public Dictionary<string, int> characterAffections = new Dictionary<string, int>();

    public List<SaveState> saveStates;

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
            var es3File = new ES3File("SaveStates.es3");
            saveStates = es3File.Load<List<SaveState>>("saveStates", new List<SaveState>() 
            {
                new SaveState(),
                new SaveState(),
                new SaveState()
            });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
            if (characterAffections.TryGetValue(characterKey, out int currentAffection))
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

    public void SaveGameState(int index)
    {
        //tbd, for saving to local file with easysave
        SaveState saveState = new SaveState()
        {
            lastSavedPath = currentPath,
            lastSavedCharacterKey = lastSavedCharacterKey,
            isDialogueSkippable = isDialogueSkippable,
            wasLastSpeakerMC = wasLastSpeakerMC,
            characterAffections = characterAffections
        };

        saveStates[index] = saveState;
        var es3File = new ES3File("SaveStates.es3");
        es3File.Save<List<SaveState>>("saveStates", saveStates);
        es3File.Sync();
    }

    public bool IsOptionsOpen()
    {
        return optionsPanel.activeSelf;
    }

    public class SaveState
    {
        public string lastSavedPath = Globals.INVALID_STRING;

        public string lastSavedCharacterKey = "";

        public bool isDialogueSkippable = true;

        public bool wasLastSpeakerMC = true;

        public Dictionary<string, int> characterAffections = new Dictionary<string, int>();
    }
}
