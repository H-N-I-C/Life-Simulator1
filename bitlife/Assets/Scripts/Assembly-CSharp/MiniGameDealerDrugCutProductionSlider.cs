using System;
using UnityEngine;

public class MiniGameDealerDrugCutProductionSlider : MonoBehaviour
{
	public int Value;

	public string Title;

	private bool _initialized;

	private int _minValue;

	private int _maxValue;

	private float mWidth;

	private GameObject _containerObject;

	private GameObject _valueLabelObject;

	public GameObject SliderObject;

	private DateTime _lastExceededTime;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, string pTitle, int pValue, int pMinValue, int pMaxValue)
	{
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}
}
