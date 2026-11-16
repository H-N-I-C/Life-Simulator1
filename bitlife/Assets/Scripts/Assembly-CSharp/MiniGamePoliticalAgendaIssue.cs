using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGamePoliticalAgendaIssue : MonoBehaviour
{
	public AccessibleButton uapButton;

	public bool mActive;

	public int mValue;

	public string mTitle;

	private bool mInitialized;

	private int mID;

	private int mMaxValue;

	private float mWidth;

	private float mNormalHeight;

	private float mExpandedHeight;

	private float mNormalHeightContainerCenter;

	private GameObject mBackerObject;

	private GameObject mContainerObject;

	public TextMeshProUGUI mHoursLabel;

	public TextMeshProUGUI lIssueLabel;

	public Slider mSlider;

	private DateTime mLastExceededTime;

	private MiniGamePoliticalAgenda parentGame;

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

	public void InitWithParameters(float pWidth, string pTitle, int pValue, int pMaxValue, ScenarioMinigameTextRegistry pTextRegistry, string tTitle)
	{
	}

	public void HandleOnClick()
	{
	}

	public void AdjustValue(GameObject pSliderObject)
	{
	}

	public void SetActive(bool pActive)
	{
	}
}
