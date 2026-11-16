using TMPro;
using UnityEngine;

public class MiniGameBusinessBonusSlider : MonoBehaviour
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

	private TextMeshProUGUI _badgeInfo;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, string pTitle, float pValue, float pMinValue, float pMaxValue, string emojiString, string pBadgeInfo)
	{
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}
}
