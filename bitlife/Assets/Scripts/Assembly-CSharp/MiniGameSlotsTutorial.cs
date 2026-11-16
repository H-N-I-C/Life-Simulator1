using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameSlotsTutorial : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private MiniGameSlotsTutorialCombinationView _combinationViewPrefab;

	private RectTransform _labelRectTransform;

	private RectTransform _scrollRectTransform;

	private RectTransform _combinationsParent;

	private float _width;

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void SetHeightConstraint()
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetWidth(float width)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	private void GenerateCombinationViews()
	{
	}
}
