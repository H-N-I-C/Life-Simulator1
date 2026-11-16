using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameFinancialHeadlines : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private bool mInitialized;

	private float mWidth;

	private Scenario mScenario;

	private MiniGameDataFinancialHeadlines mData;

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
