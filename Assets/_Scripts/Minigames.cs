using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Minigames : MonoBehaviour
{

	[SerializeField]
	MinigameEntry entries;

	public bool TryGetMinigame(string name, out Minigame value)
	{
		return entries.TryGetValue(name, out value);
	}

	[Serializable]
	public class MinigameEntry : SerializableDictionary<string, Minigame>
	{ }

}
