using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameFinancialAssetSell : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float width;

	private bool initialized;

	private Scenario scenario;

	private MiniGameDataFinancialAssetSell miniGameData;

	private GameObject backgroundObject;

	private float slidersStartVertical;

	private MiniGameFinancialAssetSellSlider slider;

	private Vector2 tablePosition;

	private GameObject tableObject;

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

	public void UpdateData()
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
