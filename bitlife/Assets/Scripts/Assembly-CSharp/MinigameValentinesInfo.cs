using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;

public class MinigameValentinesInfo : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private CandyText[] textLabels;

	private TextMeshProUGUI life18PlusTMPro;

	private TextMeshProUGUI relationshipTMPro;

	private TextMeshProUGUI sendValentineTMPro;

	private TextMeshProUGUI screenshotQRShareTMPro;

	private TextMeshProUGUI howToShareOwnCharacterTMPro;

	private TextMeshProUGUI howToGetOtherCharacterTMPro;

	private TextMeshProUGUI scanFriendQRTMPro;

	private TextMeshProUGUI downloadTMPro;

	private TextMeshProUGUI gameTMPro;

	private TextMeshProUGUI life18PlusOtherTMPro;

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
}
