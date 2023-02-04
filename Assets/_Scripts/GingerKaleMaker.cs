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

	private void Start()
	{
        StartGame();
	}

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
	}

#nullable disable

    public void PushSliderUp()
	{
        sliderInstance.GetComponent<Rigidbody2D>().AddForce(sliderPushForce);
        Debug.Log("push up");
    }
}
