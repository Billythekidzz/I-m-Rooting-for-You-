using Animatext;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AnimatextTMPro>().SetEffectSpeed(0, 5);
    }
}
