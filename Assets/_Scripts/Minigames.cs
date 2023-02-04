using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Minigames : MonoBehaviour
{

	[SerializeField]
	MinigameEntry entries;

	public Minigame this[string name]
	{
		get => entries[name];
	}

	[Serializable]
	public class MinigameEntry : SerializableDictionary<string, Minigame>
	{ }

}
