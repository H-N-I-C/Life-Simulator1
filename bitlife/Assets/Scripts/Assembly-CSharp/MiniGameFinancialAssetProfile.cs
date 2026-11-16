using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameFinancialAssetProfile : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float width;

	private bool initialized;

	private Scenario scenario;

	private MiniGameDataFinancialAssetProfile miniGameData;

	private ScenarioMinigameTextRegistry mTextRegistry;

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
