using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class GingerKaleMinigame : Minigame
{

    [Serializable]
    public struct FloatRange
    {
        public float min;
        public float max;

        public bool Test(float value)
        {
            return min <= value && value <= max;
        }
    }

    [SerializeField]
    Slider slider;

    [SerializeField]
    float fillSpeed = 1.0f;

    [SerializeField]
    FloatRange targetRange;
    

    private bool isFilling = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFilling)
		{
            FillBarOnce();
		}
    }

    public void OnPushAction(InputAction.CallbackContext context)
	{
        if (context.phase == InputActionPhase.Started)
		{
            isFilling = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
		{
            isFilling = false;
            StopFilling();
        }

    }

    private void StopFilling()
	{
        if (targetRange.Test(slider.value))
		{
            EndGame(true);
		}
	}

    private void FillBarOnce()
	{
        float value = slider.value + fillSpeed * Time.deltaTime;

        if (value > slider.maxValue)
		{
            value = slider.maxValue;
		}

        slider.value = value;
    }
}

