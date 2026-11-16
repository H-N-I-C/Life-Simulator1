using System;
using UnityEngine;

public class MiniGameZooDivisionOfLaborSlider : MonoBehaviour
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

	private string _labelString;

	private string _custodialString;

	private string _customerRelationsString;

	private bool _alertActive;

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
