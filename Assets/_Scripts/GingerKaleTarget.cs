using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerKaleTarget : MonoBehaviour
{

    public GingerKaleMaker Owner { get; set; }

    [SerializeField]
    float speed = 1f;

    [SerializeField]
    float timeBetweenMovementUpdates = 1.5f;

    private float timeSinceMovementUpdate;
    private Vector2 movement;
    private bool flippedMovement;

	private void Start()
	{
        UpdateMovement();
	}

	// Update is called once per frame
	void Update()
    {
        if (Owner.IsMiniGameActive)
		{
            UpdateTarget();
		}
    }

    void UpdateTarget()
	{
        timeSinceMovementUpdate += Time.deltaTime;
        if (timeSinceMovementUpdate >= timeBetweenMovementUpdates)
		{
            UpdateMovement();
		}

        float y = this.transform.localPosition.y + this.transform.localRotation.y / 2;

        if (!flippedMovement && (y <= 0f || y >= Owner.height))
		{
            movement *= -1;
            flippedMovement = true;
        }
        else
		{
            flippedMovement = false;
		}

        this.transform.Translate(movement * Time.deltaTime);
    }

    void UpdateMovement()
	{
        float direction = Random.value < 0.5f ? -speed : speed;

        movement = Vector2.up * direction;

        timeSinceMovementUpdate = 0f;
    }
}
