using System.Collections;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameSalon : MonoBehaviour, IguiObject
{
	private enum FACE
	{
		PREVIOUS = 0,
		NEXT = 1
	}

	private RectTransform rect;

	private float mWidth;

	private bool mInitialized;

	private Scenario mScenario;

	private bool mButtonDown;

	private bool mButtonJustPressed;

	private int mCurrentIndex;

	private int mOriginalStyle;

	private int mCurrentStyle;

	private string mOriginalColor;

	private string mCurrentColor;

	private ArrayList mOriginalDisguiseSet;

	private ArrayList mCurrentDisguiseSet;

	private GameObject mDisplayContainerObject;

	private VoltronFaceContainerView mActiveFaceView;

	private VoltronFaceContainerView mOldFaceView;

	private ArrayList mTypes;

	private ArrayList mColors;

	private GameObject mPreviousButtonObject;

	private GameObject mNextButtonObject;

	private GameObject mDescriptionLabelObject;

	private GameObject mSelectButtonObject;

	private int mSalonType;

	private ScenarioMinigameTextRegistry mMinigameRegistry;

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

	public void ButtonUpPreviousButton()
	{
	}

	public void ButtonUpNextButton()
	{
	}

	public void ButtonUpDone()
	{
	}

	public int IndexForType(int pType)
	{
		return 0;
	}

	private void UpdateLabel()
	{
	}

	private void ExitGame()
	{
	}

	private void PushDirection(int pDirection)
	{
	}

	private void KillOldFace()
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
