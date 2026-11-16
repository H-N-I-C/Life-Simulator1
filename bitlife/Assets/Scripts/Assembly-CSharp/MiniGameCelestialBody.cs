using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameCelestialBody : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float width;

	private Scenario scenario;

	private MiniGameDataCelestialBody miniGameData;

	private GameObject itemObject;

	private GameObject tableObject;

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	public void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetHeightConstraint()
	{
	}
}
