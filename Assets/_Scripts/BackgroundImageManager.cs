using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RawImage))]
public class BackgroundImageManager : MonoBehaviour
{

    public static BackgroundImageManager Instance;

    [SerializeField]
    StringTexture2DDictionary stringTexture2DDictionary;

    [SerializeField]
    RawImage backgroundImage;

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

    public void ChangeBackground(string backgroundImageKey)
    {
        if (stringTexture2DDictionary.TryGetValue(backgroundImageKey, out Texture2D value))
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(backgroundImage.DOColor(Color.black, 0.25f))
                .AppendCallback(() =>
                {
                    backgroundImage.texture = value;
                })
                .Append(backgroundImage.DOColor(Color.white, 0.25f));
            sequence.Play();
        }
    }
}
