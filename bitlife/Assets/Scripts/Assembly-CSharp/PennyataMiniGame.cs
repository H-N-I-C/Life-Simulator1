using BitLife.UI.Interfaces;
using UnityEngine;

public class PennyataMiniGame : MonoBehaviour, IguiObject
{
	private RectTransform _rect;

	private PennyataClick _pennyataClick;

	private GameObject labelReward;

	private GameObject labelUse;

	private GameObject standButtonText;

	private ScenarioScavengerHuntPrize scenario;

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	private void ExitGame()
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

	private void _003CInitWithParameters_003Eb__7_0()
	{
	}
}
