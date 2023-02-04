using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System;
using TMPro;
using Animatext;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;
    bool _isSelectingChoice;

    static Story story;
    //[Inject]
    //Story story;

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

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        nametag = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        messageAnimaTextTMPro = textBox.transform.GetChild(1).GetComponent<AnimatextTMPro>();
        messageAnimaTextTMPro = textBox.transform.GetChild(1).GetComponent<AnimatextTMPro>();
        messageAnimaTextTMPro.SetEffectSpeed(0, defaultTextSpeed);
        messageAnimaTextTMPro.SetEffectSpeed(1, defaultTextSpeed);
        tags = new List<string>();
        choiceSelected = null;
        //story.ChoosePathString("mysterious_fisherman");
        //       TryDialogue();
        SetThenTryDialogue("sheriff_beefroot");
    }

    private void TriggerDialogs(string inklePath)
    {
        SetThenTryDialogue(inklePath);
    }

    public void SetThenTryDialogue(string path)
    {
        story.ChoosePathString(path);
        SetCharacter(story.TagsForContentAtPath(path)[0]);
        TryDialogue();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)) && textBox.activeSelf)
        {
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
                if(typeSentenceCoroutine != null)
                {
                    StopCoroutine(typeSentenceCoroutine);
                }
                messageAnimaTextTMPro.StopEffect(0);
                sentenceBeingTypedOut = message.text;
            }
        }

        if(isTalking && (messageAnimaTextTMPro.effects[0].state == EffectState.End
                || messageAnimaTextTMPro.effects[0].state == EffectState.Stop))
        {
            isTalking = false;
            if(typeSentenceCoroutine != null)
            {
                StopCoroutine(typeSentenceCoroutine);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log(messageAnimaTextTMPro.effects[0].state);
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
        textBox.SetActive(false);
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        sentenceBeingTypedOut = currentSentence;
        typeSentenceCoroutine = StartCoroutine(TypeSentence());
        message.text = currentSentence;
        if (story.currentChoices.Count != 0)
        {
            StartCoroutine(ShowChoices());
        }
    }

    IEnumerator TypeSentence()
    {
        AudioElement audioElement = AudioManager.Instance.PlaySound(GameStateManager.Instance.lastSavedCharacterKey);
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

        yield return new WaitUntil(() => { return choiceSelected != null; });
        _isSelectingChoice = false;

        AdvanceFromDecision();
    }

    // Tells the story which branch to go to
    public static void SetDecision(object element)
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
                case "background":
                    ChangeBackground(param);
                    break;
                case "character":
                    SetCharacter(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
                case "text_speed":
                    SetTextSpeed(param);
                    break;
            }
        }
    }

    private void SetTextSpeed(string param)
    {
        if (float.TryParse(param, out float value))
        {
            messageAnimaTextTMPro.SetEffectSpeed(0, defaultTextSpeed * value);
        }
    }

    private void PlayAudio(string param)
    {
        AudioManager.Instance.PlaySound(param);
    }

    void ChangeBackground(string param)
    {
        StoryboardManager.Instance.ChangeBackground(param);
    }

    void SetCharacter(string _character)
    {
        GameStateManager.Instance.lastSavedCharacterKey = _character;
        nametag.text = _character;
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
