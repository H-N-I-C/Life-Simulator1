using UnityEngine;

public class MiniGameBusinessPayrollSlider : MonoBehaviour
{
	public bool mActive;

	public int mValue;

	public string mTitle;

	private bool mInitialized;

	private int mMinValue;

	private int mMaxValue;

	private float mWidth;

	private GameObject mContainerObject;

	private GameObject mValueLabelObject;

	public GameObject mSliderObject;

	private SimBusiness mBusiness;

	private ScenarioBusinessPayroll mscenario;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, string pTitle, int pValue, int pMinValue, int pMaxValue, string emojiString, string translatedTitle, ScenarioBusinessPayroll scenario)
	{
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}

	public void SnapToPosition(GameObject pSliderObject)
	{
	}
}
