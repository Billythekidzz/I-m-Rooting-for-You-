using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour
{

	[SerializeField]
	string character;

	[SerializeField]
	int affinityDelta;

	public bool IsGameActive { get; private set; }

	public UnityEvent<GameOverContext> onGameOver;

	public virtual void StartGame()
	{
		IsGameActive = true;
		GameStateManager.Instance.OnMiniGameStart();
	}

	public virtual void EndGame(bool isVictory)
	{
		if (IsGameActive)
		{
			GameOverContext context = new GameOverContext(isVictory, character, affinityDelta);

			onGameOver.Invoke(context);
			GameStateManager.Instance.OnMinigameFinish(context);
		}

		IsGameActive = false;
	}


	public class GameOverContext
	{
		public bool IsVictory { get; }

		public string Character { get; }

		public int AffinityDelta { get; }

		public GameOverContext(bool isVictory, string character, int affinityDelta)
		{
			this.IsVictory = isVictory;
			this.Character = character;
			this.AffinityDelta = affinityDelta;
		}
	}

}
