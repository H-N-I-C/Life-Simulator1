using UnityEngine;

public class MiniGameBusinessProductScheduleSlider : MonoBehaviour
{
	public bool mActive;

	public float mValue;

	public string mTitle;

	private bool mInitialized;

	private bool mInteractable;

	private float mMinValue;

	private float mMaxValue;

	private float mWidth;

	private GameObject mContainerObject;

	private GameObject mValueLabelObject;

	public GameObject mSliderObject;

	private SimProduct mProduct;

	private string mFixedCostString;

	private string mMarketingCostString;

	private string mPriceString;

	private string mProductionString;

	private ScenarioMinigameTextRegistry mTextRegistry;

	private bool mAlertActive;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, string pTitle, float pValue, float pMinValue, float pMaxValue, bool pInteractable, SimProduct product, string emojiString, ScenarioMinigameTextRegistry minigameTextRegistry)
	{
	}

	public double GetRoundedPrice(double number)
	{
		return 0.0;
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}

	public void FixedCostAlert()
	{
	}

	private void _003CFixedCostAlert_003Eb__24_0(int index)
	{
	}
}
