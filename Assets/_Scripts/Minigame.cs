using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour
{
	public bool IsGameActive { get; private set; }

	public UnityEvent<GameOverContext> onGameOver;

	public virtual void StartGame()
	{
		IsGameActive = true;
	}

	public virtual void EndGame(bool isVictory)
	{
		if (IsGameActive)
		{
			GameOverContext context = new GameOverContext(isVictory);

			onGameOver.Invoke(context);
		}

		IsGameActive = false;
	}


	public class GameOverContext
	{
		public bool IsVictory { get; }

		public GameOverContext(bool isVictory)
		{
			this.IsVictory = isVictory;
		}
	}

}
