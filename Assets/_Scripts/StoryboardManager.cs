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

    public void ChangeCharacter(string characterImageKey)
    {
        if (stringCharacterImageElementDictionary.TryGetValue(characterImageKey, out ImageElement value))
        {
            backgroundImage.sprite = value.sprites[0];
        }
        else
        {
            characterImage.DOFade(0, 1.0f);
        }
    }

    private void OnCharacterChange()
    {
        ChangeCharacter(GameStateManager.Instance.lastSavedCharacterKey);
    }
}
