using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System;
using TMPro;
using Animatext;
using UnityEngine.InputSystem;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;
    bool _isSelectingChoice;

    [SerializeField]
    GameObject gameOverScreen;

    static Story story;
    //[Inject]
    //Story story;

    [SerializeField]
    Image textBoxImage;

    [SerializeField]
    Sprite textBoxNoNameTag;

    [SerializeField]
    Sprite textBoxWithNameTag;

    TextMeshProUGUI nametag;
    TextMeshProUGUI message;
    AnimatextTMPro messageAnimaTextTMPro;

    List<string> tags;
    static Choice choiceSelected;

    [SerializeField]
    float defaultTextSpeed = 1.0f;

    string sentenceBeingTypedOut = "";

    Coroutine typeSentenceCoroutine;

    [SerializeField]
    float speakAudioWaitDelay = 0.5f;

    [SerializeField]
    Minigames minigameRegistry;

    private Minigame currentlyPlaying = null;

    [SerializeField]
    string startingPath = "Preamble";

    [SerializeField]
    Image mouseIcon;

    public static DialogueManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        nametag = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        messageAnimaTextTMPro = textBox.transform.GetChild(1).GetComponent<AnimatextTMPro>();
        messageAnimaTextTMPro.SetEffectSpeed(0, defaultTextSpeed);
        messageAnimaTextTMPro.SetEffectSpeed(1, defaultTextSpeed);
        tags = new List<string>();
        choiceSelected = null;
        //story.ChoosePathString("mysterious_fisherman");
        //       TryDialogue();
    }

    public void StartNewGame()
    {
        GameStateManager.Instance.ResetGameState();
        RestartAndLoadPath(startingPath);
    }

    public void RestartAndLoadPath(string path)
    {
        gameOverScreen.SetActive(false);
        story = new Story(inkFile.text);
        messageAnimaTextTMPro.SetEffectSpeed(0, defaultTextSpeed);
        messageAnimaTextTMPro.SetEffectSpeed(1, defaultTextSpeed);
        choiceSelected = null;
        if (typeSentenceCoroutine != null)
        {
            StopCoroutine(typeSentenceCoroutine);
        }
        messageAnimaTextTMPro.StopEffect(0);
        SetThenTryDialogue(path);
    }

    private void TriggerDialogs(string inklePath)
    {
        SetThenTryDialogue(inklePath);
    }

    public void SetThenTryDialogue(string path)
    {
        story.ChoosePathString(path);
        if (story.TagsForContentAtPath(path) != null)
        {
            SetCharacter(story.TagsForContentAtPath(path)[0]);
        }
        TryDialogue();
    }

    public void OnNextAction(InputAction.CallbackContext context)
	{

        if (!textBox.activeSelf || context.phase != InputActionPhase.Started || GameStateManager.Instance.IsOptionsOpen())
		{
            return;
		}

        if (messageAnimaTextTMPro.effects[0].state == EffectState.End
                || messageAnimaTextTMPro.effects[0].state == EffectState.Stop
                || sentenceBeingTypedOut == "")
        {
            if (!_isSelectingChoice)
            {
                TryDialogue();
            }
        }
        else
        {
            if (GameStateManager.Instance.isDialogueSkippable)
            {
                if (typeSentenceCoroutine != null)
                {
                    StopCoroutine(typeSentenceCoroutine);
                }
                messageAnimaTextTMPro.StopEffect(0);
                sentenceBeingTypedOut = message.text;
            }
        }
    }

    private void Update()
    {
        if(isTalking && (messageAnimaTextTMPro.effects[0].state == EffectState.End
                || messageAnimaTextTMPro.effects[0].state == EffectState.Stop))
        {
            isTalking = false;
            if(typeSentenceCoroutine != null)
            {
                StopCoroutine(typeSentenceCoroutine);
            }
            mouseIcon.DOFade(1.0f, 0.1f);
        }
    }

    void TryDialogue()
    {
        //Is there more to the story?
        if (story.canContinue)
        {
            //nametag.text = story.TagsForContentAtPath("mysterious_fisherman")[0];
            textBox.SetActive(true);
            AdvanceDialogue();
        }
        else
        {
            FinishDialogue();
        }
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
        GameStateManager.Instance.ToggleOptionsPanel();
        AudioManager.Instance.PlaySound("objection");
        gameOverScreen.SetActive(true);
        textBox.SetActive(false);
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        sentenceBeingTypedOut = currentSentence;
        mouseIcon.DOFade(0f, 0.05f);
        typeSentenceCoroutine = StartCoroutine(TypeSentence());
        message.text = currentSentence;
        if (story.currentChoices.Count != 0)
        {
            StartCoroutine(ShowChoices());
        }
    }

    IEnumerator TypeSentence()
    {
        AudioElement audioElement = AudioManager.Instance.PlaySound(GameStateManager.Instance.wasLastSpeakerMC ? "MC_THOUGHTS" : GameStateManager.Instance.lastSavedCharacterKey);
        yield return new WaitForSeconds(audioElement == null ? speakAudioWaitDelay : audioElement.delay);
        isTalking = true;
        typeSentenceCoroutine = StartCoroutine(TypeSentence());
    }

    // Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        _isSelectingChoice = true;
        //Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => {

            if (currentlyPlaying != null)
			{
                return !currentlyPlaying.IsGameActive;
            }

            return choiceSelected != null; 
        });
        _isSelectingChoice = false;

        if (currentlyPlaying && currentlyPlaying.gameObject)
        {
            currentlyPlaying.onGameOver.RemoveListener(OnMinigameComplete);
            currentlyPlaying.gameObject.SetActive(false);
        }
        currentlyPlaying = null;

        AdvanceFromDecision();
    }

    // Tells the story which branch to go to
    public static void SetDecision(object element) // why the fuck is this object
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
                case "audio":
                    PlayAudio(param);
                    break;
                case "music":
                    PlayMusic(param);
                    break;
                case "background":
                    ChangeBackground(param);
                    break;
                case "character":
                    SetCharacter(param);
                    break;
                case "emote":
                    SetEmotion(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
                case "text_speed":
                    SetTextSpeed(param);
                    break;
                case "set_skippable_dialogue":
                    SetDialogSkipping(param);
                    break;
                case "minigame":
                    SetMinigame(param);
                    break;
                case "add_affinity":
                    AddAffinity(param);
                    break;
                case "savepoint":
                    SetSavePoint(param);
                    break;
                case "expand_textbox":
                    PlayTextboxSurpriseAnim(param);
                    break;
            }
        }
    }

    private void SetSavePoint(string param)
    {
        GameStateManager.Instance.SetSavePoint(param);
    }

    private void SetEmotion(string param)
    {
        StoryboardManager.Instance.SetEmotion(param);
    }

    private void AddAffinity(string param)
    {
        GameStateManager.Instance.AddAffinity(param);
    }

    private void SetMinigame(string param)
	{
        Minigame game;
        if (minigameRegistry.TryGetMinigame(param, out game))
		{
            currentlyPlaying = game;
            currentlyPlaying.gameObject.SetActive(true);
            currentlyPlaying.onGameOver.AddListener(OnMinigameComplete);
        }
		else
		{
            Debug.LogError($"Unknown minigame: {param}");
        }
    }

    private void OnMinigameComplete(Minigame.GameOverContext context)
	{
        int affinity = context.AffinityDelta * (context.IsVictory ? 1 : -1);

        string param = $"{context.Character}|{affinity}";

        GameStateManager.Instance.AddAffinity(param);
    }

    private void SetTextSpeed(string param)
    {
        if (float.TryParse(param, out float value))
        {
            messageAnimaTextTMPro.SetEffectSpeed(0, defaultTextSpeed * value);
        }
    }

    private void SetDialogSkipping(string param)
    {
        if (bool.TryParse(param, out bool value))
        {
            GameStateManager.Instance.isDialogueSkippable = value;
        }
    }

    private void PlayAudio(string param)
    {
        AudioManager.Instance.PlaySound(param);
    }

    private void PlayMusic(string param)
    {
        AudioManager.Instance.PlayMusic(param);
    }

    void ChangeBackground(string param)
    {
        StoryboardManager.Instance.ChangeBackground(param);
    }

    void SetCharacter(string _character)
    {
        GameStateManager.Instance.SetCharacter(_character);
        if (_character == Globals.MC_KEY || _character == Globals.MC_THOUGHTS_KEY)
        {

            textBoxImage.sprite = textBoxNoNameTag;
            nametag.text = "";
        }
        else
        {
            textBoxImage.sprite = textBoxWithNameTag;
            nametag.text = _character;
        }
    }

    void PlayTextboxSurpriseAnim(string param)
    {
        textBoxImage.rectTransform.DOPunchScale(new Vector3(0.4f, 0.2f, 0), 0.3f, 10, 1);
    }

    void SetAnimation(string _name)
    {
        CharacterPortrait cs = GameObject.FindObjectOfType<CharacterPortrait>();
        cs.PlayAnimation(_name);
    }
    void SetTextColor(string _color)
    {
        switch (_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }

}
