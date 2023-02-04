using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class OnionRing : MonoBehaviour
{
	public OnionRingGame Owner { get; set; }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "OnionRingBoundary")
		{
			Owner.OnRingDestroyed(false);
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "OnionRingTarget")
		{
			Owner.OnRingDestroyed(true);
			Destroy(this.gameObject);
		}
	}
}
