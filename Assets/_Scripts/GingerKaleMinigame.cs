using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class GingerKaleMinigame : Minigame
{

    [SerializeField]
    Slider slider;

    [SerializeField]
    float fillSpeed = 1.0f;

    [SerializeField]
    float minFill = 0.97f;

    private bool isFilling = false;

	private void OnEnable()
	{
        StartGame();
	}

	private void OnDisable()
	{
        if (IsGameActive)
		{
            EndGame(false);
        }
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

        if (!IsGameActive)
		{
            return;
		}

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
        if (slider.value >= minFill)
		{
            EndGame(true);
            
		} else if (slider.value >= 0.1f)
		{
            EndGame(false);
		}

        Debug.Log($"value = {slider.value}");
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

