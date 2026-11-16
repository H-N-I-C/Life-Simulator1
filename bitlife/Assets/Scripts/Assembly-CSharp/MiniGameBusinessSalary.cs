using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameBusinessSalary : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private GameObject mTableObject;

	private bool mInitialized;

	private bool mButtonJustPressed;

	private float mWidth;

	private float mSlidersStartVertical;

	private Scenario mScenario;

	private MiniGameDataBusinessSalary mData;

	private MiniGameBusinessSalarySlider mSlider;

	private Vector2 mTablePosition;

	private SimBusiness mBusiness;

	private float mCurrentAnnualPayroll;

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

	public float PositionSlider()
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
}
