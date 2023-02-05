using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

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

    public List<SaveState> saveStates;

    public SaveState saveStateToSave = new SaveState();

    [SerializeField]
    GameObject optionsPanel;

    [SerializeField]
    RectTransform optionsPanelTooltip;

    Tween tween;
    Tween tween2;
    Tween tween3;

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
        if (optionsPanel.activeSelf)
        {
            tween?.Kill();
            tween2?.Kill();
            tween3?.Kill();
            tween = optionsPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -1358, 0), 0.5f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    optionsPanel.SetActive(false);
                });
            Color color = optionsPanelTooltip.GetComponent<TextMeshProUGUI>().color;
            color.a = 1.0f;
            optionsPanelTooltip.GetComponent<TextMeshProUGUI>().color = color;
            tween3 = optionsPanelTooltip.GetComponent<TextMeshProUGUI>().DOFade(0.3f, 0.5f).SetDelay(1.0f);
            tween2 = optionsPanelTooltip.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -125, 0), 1.0f).SetEase(Ease.OutBack);
        }
        else
        {
            optionsPanel.SetActive(true);
            tween?.Kill();
            tween2?.Kill();
            tween = optionsPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -258, 0), 0.5f).SetEase(Ease.OutBack);
            tween2 = optionsPanelTooltip.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -14, 0), 0.5f).SetEase(Ease.OutBack);
        }
        AudioManager.Instance.PlaySound("OPTIONS_PANEL");
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

    public void SetSavePoint(string savePointPath)
    {
        saveStateToSave = new SaveState()
        {
            lastSavedPath = savePointPath,
            characterAffections = new Dictionary<string, int>(characterAffections)
        };
    }


    public void SaveGameState(int index)
    {
        saveStates[index] = saveStateToSave;
        var es3File = new ES3File("SaveStates.es3");
        es3File.Save<List<SaveState>>("saveStates", saveStates);
        es3File.Sync();
    }

    public void LoadGameState(int saveIndex)
    {
        ResetGameState();
        SaveState saveStateToLoad = saveStates[saveIndex];
        characterAffections = new Dictionary<string, int>(saveStateToLoad.characterAffections);
        DialogueManager.Instance.RestartAndLoadPath(saveStateToLoad.lastSavedPath);
        ToggleOptionsPanel();
    }

    public void ResetGameState()
    {
        isDialogueSkippable = true;
        lastSavedCharacterKey = "";
    }

    public bool IsOptionsOpen()
    {
        return optionsPanel.activeSelf;
    }

    public void StartNewGame()
    {
        DialogueManager.Instance.StartNewGame();
        ToggleOptionsPanel();
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
