using UnityEngine;

public class MiniGameFinancialAssetSellSlider : MonoBehaviour
{
	public bool active;

	public float value;

	public string title;

	private bool initialized;

	private float minValue;

	private float maxValue;

	private float width;

	private GameObject containerObject;

	private GameObject valueLabelObject;

	private GameObject sliderObject;

	private bool isCurrency;

	private double increment;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, string pTitle, double pValue, double pMinValue, double pMaxValue, string emojiString, bool useCurrency, double forcedIncrement)
	{
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}

	private float GetRoundedValue(float number, float comparisonNumber)
	{
		return 0f;
	}
}
