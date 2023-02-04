using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour
{
    public bool IsGameActive { get; private set; }

	[SerializeField]
	UnityEvent<bool> onGameOver;

    public virtual void StartGame()
	{
		IsGameActive = true;
	}

	public virtual void EndGame(bool isVictory)
	{
		IsGameActive = false;
		onGameOver.Invoke(isVictory);
	}


}
