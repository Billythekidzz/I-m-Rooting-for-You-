using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GingerKaleMaker : MonoBehaviour
{

    [SerializeField]
    GameObject sliderPrefab;

    [SerializeField]
    Vector2 sliderPushForce;

    [SerializeField]
    Canvas gameCanvas;

#nullable enable
	private GameObject? sliderInstance;

    public void StartGame()
	{
        if (sliderInstance != null)
		{
            Destroy(sliderInstance);
		}

        sliderInstance = Instantiate(sliderPrefab);
        sliderInstance.transform.SetParent(this.transform);

        // setting canvas to screenspace: camera makes mini game draw on top of canvas
	}

    public void EndGame()
	{
        if (sliderInstance != null)
        {
            Destroy(sliderInstance);
        }
    }

#nullable disable

    public void PushSliderUp()
	{
        sliderInstance.GetComponent<Rigidbody2D>().AddForce(sliderPushForce);
    }
}
