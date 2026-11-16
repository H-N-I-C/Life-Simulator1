using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameLicense : MonoBehaviour, IguiObject
{
	public enum LICENSE_RESULT
	{
		WIN = 0,
		LOSE = 1
	}

	private RectTransform rect;

	private float mWidth;

	private bool mInitialized;

	private Scenario mScenario;

	private bool mButtonDown;

	private bool mButtonJustPressed;

	private GameObject mImageObject;

	private GameObject mAButtonObject;

	private GameObject mBButtonObject;

	private GameObject mCButtonObject;

	private int mLicenseType;

	private string mCorrectAnswerString;

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

	public void ButtonUpA()
	{
	}

	public void ButtonUpB()
	{
	}

	public void ButtonUpC()
	{
	}

	private void GameOverWithType(int pType)
	{
	}

	private void ExitGame()
	{
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
