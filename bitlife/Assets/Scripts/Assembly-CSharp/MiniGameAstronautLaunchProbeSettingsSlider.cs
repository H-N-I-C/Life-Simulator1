using System;
using UnityEngine;

public class MiniGameAstronautLaunchProbeSettingsSlider : MonoBehaviour
{
	public bool active;

	public float value;

	public string title;

	private bool initialized;

	private float minValue;

	private float maxValue;

	private float theWidth;

	private GameObject containerObject;

	private GameObject valueLabelObject;

	private GameObject sliderObject;

	private DateTime lastExceededTime;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, string pTitle, float pValue, float pMinValue, float pMaxValue, string emojiString)
	{
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}
}
