using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class StoryboardManager : MonoBehaviour
{
    public static StoryboardManager Instance;

    [SerializeField]
    StringImageElementListDictionary stringBackgroundImageElementDictionary;

    [SerializeField]
    StringImageElementListDictionary stringCharacterImageElementDictionary;

    [SerializeField]
    Image backgroundImage;

    [SerializeField]
    Image characterImage;

    Sequence sequence;

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

    private void Start()
    {
        GameStateManager.Instance.characterChangedEvent.AddListener(OnCharacterChange);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.characterChangedEvent.RemoveListener(OnCharacterChange);
    }

    public void ChangeBackground(string backgroundImageKey)
    {
        if (stringBackgroundImageElementDictionary.TryGetValue(backgroundImageKey, out ImageElement value))
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(backgroundImage.DOColor(Color.black, 0.25f))
                .AppendCallback(() =>
                {
                    backgroundImage.sprite = value.sprites[0];
                })
                .Append(backgroundImage.DOColor(Color.white, 0.25f));
            sequence.Play();
        }
    }

    string lastChangedCharacterKey = "";
    public void ChangeCharacter(string characterImageKey)
    {
        if (stringCharacterImageElementDictionary.TryGetValue(characterImageKey, out ImageElement value))
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            if (lastChangedCharacterKey == Globals.MC_THOUGHTS_KEY)
            {
                Color color = characterImage.color;
                color.a = 0;
                characterImage.color = color;
                characterImage.sprite = value.sprites[0];
                sequence.Append(characterImage.DOFade(1, 0.5f));
            }
            else
            {
                sequence.Append(characterImage.DOFade(0, 0.5f))
                .AppendCallback(() =>
                {
                    characterImage.sprite = value.sprites[0];
                    characterImage.rectTransform.DOPunchScale(new Vector3(0f, 0.1f, 0), 0.5f).SetDelay(0.1f);
                }).Append(characterImage.DOFade(1, 0.5f)
            );
            }
            
        }
        else
        {
            characterImage.DOFade(0, 1.0f);
        }
        lastChangedCharacterKey = characterImageKey;
    }

    private void OnCharacterChange()
    {
        ChangeCharacter(GameStateManager.Instance.lastSavedCharacterKey);
    }

    public void SetEmotion(string param)
    {
        int emoteIndex = Globals.EMOTE_NEUTRAL_INDEX;
        string emoteString = null;
        if(param.ToLower() == "sad")
        {
            emoteIndex = Globals.EMOTE_SAD_INDEX;
            emoteString = "SAD_EMOTE";
        }
        else if (param.ToLower() == "happy")
        {
            emoteIndex = Globals.EMOTE_HAPPY_INDEX;
            emoteString = "HAPPY_EMOTE";
        }
        else if (param.ToLower() == "blush")
        {
            emoteIndex = Globals.EMOTE_BLUSH_INDEX;
            emoteString = "BLUSH_EMOTE";
        }
        else if (param.ToLower() == "cry")
        {
            emoteIndex = Globals.EMOTE_CRY_INDEX;
            emoteString = "SAD_EMOTE";
        }
        else if (param.ToLower() == "neutral2")
        {
            emoteIndex = Globals.EMOTE_NEUTRAL2_INDEX;
        }
        if (stringCharacterImageElementDictionary.TryGetValue(GameStateManager.Instance.lastSavedCharacterKey, out ImageElement value))
        {
            characterImage.rectTransform.DOPunchScale(new Vector3(0f, 0.1f, 0), 0.5f);
            if (emoteIndex < value.sprites.Length)
            {
                characterImage.sprite = value.sprites[emoteIndex];
                if(emoteString != null)
                {
                    AudioManager.Instance.PlaySound(emoteString);
                }
            }
        }
    }
}
