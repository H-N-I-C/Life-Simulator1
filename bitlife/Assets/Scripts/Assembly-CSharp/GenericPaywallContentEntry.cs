using BitLife.UI.CandyObjects;
using UnityEngine;

public class GenericPaywallContentEntry : MonoBehaviour
{
	private const float MARGIN = 30f;

	public CandyImage backgroundImage;

	public CandyImage iconImage;

	public CandyText textLabel;

	private RectTransform _rectTransform;

	public void Setup(GenericPaywallContentSO.PaywallContentItem contentItem, Color backgroundColor)
	{
	}

	public void AdjustToTextSize()
	{
	}
}
