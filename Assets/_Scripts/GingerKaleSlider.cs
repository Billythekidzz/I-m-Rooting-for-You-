using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerKaleSlider : MonoBehaviour
{

	private static readonly string TARGET_TAG = "GingerKaleTarget";


	public GingerKaleMaker Owner { get; set; }

	public float timeInTarget;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == TARGET_TAG)
		{
			timeInTarget += Time.deltaTime;
			if (timeInTarget >= Owner.targetStayTime)
			{
				Owner.EndGame();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == TARGET_TAG)
		{
			timeInTarget = 0f;
		}
	}
}
