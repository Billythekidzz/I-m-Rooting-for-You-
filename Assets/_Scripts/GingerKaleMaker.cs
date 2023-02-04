using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GingerKaleMaker : MonoBehaviour
{

	[SerializeField]
	GameObject sliderPrefab;

	[SerializeField]
	GameObject targetPrefab;

	[SerializeField]
	Vector2 targetStartPos;

	[SerializeField]
	Vector2 sliderPushForce;

	public float height = 5f;

	public float targetStayTime;

	public bool IsMiniGameActive { get; private set; }

#nullable enable
	private Rigidbody2D? sliderRb;
	public GingerKaleTarget? targetInstance;

	public void StartGame()
	{
		if (sliderRb != null)
		{
			Destroy(sliderRb.gameObject);
		}

		// create target

		if (targetInstance != null)
		{
			Destroy(targetInstance.gameObject);
		}

		targetInstance = Instantiate(targetPrefab).GetComponent<GingerKaleTarget>();
		targetInstance.Owner = this;
		targetInstance.transform.SetParent(this.transform);
		targetInstance.transform.position = this.targetStartPos;


		// create slider 
		var slider = Instantiate(sliderPrefab);
		slider.GetComponent<GingerKaleSlider>().Owner = this;
		slider.transform.SetParent(this.transform);

		sliderRb = slider.GetComponent<Rigidbody2D>();

		IsMiniGameActive = true;
		// setting canvas to screenspace: camera makes mini game draw on top of canvas
	}

	public void EndGame()
	{
		if (sliderRb != null)
		{
			Destroy(sliderRb.gameObject);
		}
		
		if (targetInstance != null)
		{
			Destroy(targetInstance.gameObject);
		}

		IsMiniGameActive = false;
	}

	public void PushSliderUp()
	{
		if (sliderRb != null)
		{
			sliderRb.AddForce(sliderPushForce);
		}
	}

#nullable disable
}
