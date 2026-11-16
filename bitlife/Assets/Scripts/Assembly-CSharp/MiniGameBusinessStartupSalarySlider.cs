using UnityEngine;

public class MiniGameBusinessStartupSalarySlider : MonoBehaviour
{
	public bool mActive;

	public float mValue;

	public string mTitle;

	private bool mInitialized;

	private float mMinValue;

	private float mMaxValue;

	private float mWidth;

	private GameObject mContainerObject;

	private GameObject mValueLabelObject;

	private GameObject mSliderObject;

	private SimBusiness mBusiness;

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
