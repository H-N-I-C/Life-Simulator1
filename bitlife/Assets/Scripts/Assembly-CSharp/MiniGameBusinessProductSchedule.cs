using System.Collections;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameBusinessProductSchedule : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private GameObject mTableObject;

	private bool mInitialized;

	private bool mButtonJustPressed;

	private float mWidth;

	private float mSlidersStartVertical;

	private Scenario mScenario;

	private MiniGameDataBusinessProductSchedule mData;

	private ArrayList mSliders;

	private Vector2 mTablePosition;

	private SimBusiness mBusiness;

	private SimProduct mProduct;

	private float mCurrentCosts;

	private string mFixedCostString;

	private string mMarketingCostString;

	private string mPriceString;

	private string mProductionString;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	public float PositionSliders()
	{
		return 0f;
	}

	public void ButtonUp()
	{
	}

	public void ButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	public void GameOver()
	{
	}

	public void ExitGame()
	{
	}

	public void RebuildTable()
	{
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}

	private void _003CButtonUp_003Eb__23_0(int index)
	{
	}
}
