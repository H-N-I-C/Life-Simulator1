using System.Collections;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGamePorchPirate : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private bool mInitialized;

	private bool mGameOver;

	private bool mButtonJustPressed;

	private int mHouseSelected;

	private Scenario mScenario;

	private float mWidth;

	private ArrayList mHouseButtons;

	private GameObject mExitButton;

	private MiniGameDataPorchPirate mMiniGameData;

	private ScenarioMinigameTextRegistry mTextRegistry;

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

	private void GameOver(int pType)
	{
	}

	private void ExitGame()
	{
	}

	public void ButtonUpHouse(GameObject pObject)
	{
	}

	public void ButtonUpExit()
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
