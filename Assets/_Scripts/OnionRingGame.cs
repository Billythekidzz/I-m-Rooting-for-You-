using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using DG.Tweening;

public class OnionRingGame : Minigame
{

	[SerializeField]
	GameObject tutorialTooltip;

	[Serializable]
	public class OnionRingSpawner
	{
		[SerializeField]
		float spawnOffset;

		[SerializeField]
		GameObject prefab;

		public GameObject Spawn(Transform parent)
		{
			var ringObj = Instantiate(prefab, parent);

			//ringObj.transform.SetParent(parent);

			ringObj.transform.Translate(new Vector2(spawnOffset, 0));

			return ringObj;
		}
	}

	[SerializeField]
	List<OnionRingSpawner> rings;

	[SerializeField]
	Transform spawnPoint;

	[SerializeField]
	float ringSpawnDelay = 5f;

	[SerializeField]
	float percentNeededForVictory = 0.9f;

	[SerializeField]
	Vector2 pushPower = new Vector2(1.0f, 1.0f);

	private int ringsHit = 0;

	private int lastSpawnedRing = -1;

	private float ringSpawnTimer;

	private Rigidbody2D lastRingSpawnedRb;

	private bool spawnNextRing;

	private void OnEnable()
	{
		StartGame();
	}

	private void OnDisable()
	{
		EndGame(false);
	}

	public override void StartGame()
	{
		base.StartGame();
		lastSpawnedRing = -1;
		ringsHit = 0;
		ringSpawnTimer = 0f;
		lastRingSpawnedRb = null;
		spawnNextRing = true;
		tutorialTooltip.SetActive(true);
	}

    public override void EndGame(bool isVictory)
    {
        base.EndGame(isVictory);
		tutorialTooltip.SetActive(false);
	}

    public void OnMoveRing(InputAction.CallbackContext context)
	{
		// todo: move ring side to side

		if (!IsGameActive)
		{
			return;
		}

		if (context.phase != InputActionPhase.Started)
		{
			return;
		}

		if (lastRingSpawnedRb == null)
		{
			return;
		}

		var input = context.ReadValue<Vector2>();
		var force = new Vector2(input.x, 1f) * pushPower;
		lastRingSpawnedRb.AddForce(force);
		//this should be done in onion ring script not here technically (:
		lastRingSpawnedRb.transform.DOPunchScale(new Vector3(0.05f, 0.1f, 0), 0.25f);
		AudioManager.Instance.PlaySound("OPTIONS_PANEL");
	}

	public void OnRingDestroyed(bool hitTarget)
	{
		if (hitTarget)
		{
			ringsHit++;
			StoryboardManager.Instance.SetEmotion("happy");
		}
		else
        {
			StoryboardManager.Instance.SetEmotion("sad");
		}
		spawnNextRing = true;
	}

	private void Update()
	{
		if (IsGameActive)
		{
			UpdateRingSpawner();
		}
	}

	private void UpdateRingSpawner()
	{
		ringSpawnTimer = spawnNextRing ? ringSpawnTimer + Time.deltaTime : 0f;

		if (spawnNextRing && ringSpawnTimer >= ringSpawnDelay)
		{
			lastSpawnedRing++;
			if (lastSpawnedRing < rings.Count)
			{
				var ringType = rings[lastSpawnedRing];

				var ring = ringType.Spawn(this.spawnPoint);
				lastRingSpawnedRb = ring.GetComponent<Rigidbody2D>();

				var ringScript = ring.GetComponent<OnionRing>();

				if (ringScript != null)
				{
					ringScript.Owner = this;
					onGameOver.AddListener((_) =>
						{
							if (ringScript != null)
							{
								Destroy(ringScript.gameObject);
							}
						}
					);
				}

				spawnNextRing = false;
			}
			else
			{
				OnRingsComplete();
			}

			ringSpawnTimer = 0f;
		}
	}

	private void OnRingsComplete()
	{
		bool isVictory = ringsHit >= rings.Count * percentNeededForVictory;

		EndGame(isVictory);
	}
}
