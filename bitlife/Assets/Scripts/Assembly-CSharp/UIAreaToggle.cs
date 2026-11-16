using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.Events;

public class UIAreaToggle : MonoBehaviour
{
	private static float FEATURE_ONLY_HEIGHT;

	private static float NO_TITLE_POS;

	private static float TOP_SPACE;

	private CandyButton button;

	private AccessibleCandyButton uapButton;

	private CandyImage background;

	private CandyImage backgroundBorder;

	private RectTransform logoContainer;

	private GameObject logoBitizen;

	private GameObject logoGodMode;

	private GameObject logoPlus;

	private CandyImage checkBox;

	private CandyImage checkBackingTop;

	private CandyImage checkBackingBottom;

	private CandyText title;

	private CandyText priceTag;

	private CandyText priceTagMarkOut;

	private RectTransform priceContainer;

	private string originalAdRemovalTitle;

	private Vector2 originalAdRemovalTitlePosition;

	private IAP_TYPE iapType;

	private Sprite imageCheckActive;

	private Sprite imageCheckInactive;

	private Color colorBgActive;

	private Color colorBgInactive;

	private Color colorBorderActive;

	private Color colorBorderInactive;

	private Color colorCheckBackingActive;

	private Color colorCheckBackingInactive;

	public IAP_TYPE IAPType => IAP_TYPE.NONE;

	public void SetAdRemovalWording(bool forcedAds, float extraHeight)
	{
	}

	public void SetLogoPos(float y)
	{
	}

	public void SetPriceColor(string hex)
	{
	}

	public void Setup(string price, float runningHeight = 0f, float topSpace = 0f)
	{
	}

	public void Setup(bool isBitizenPaywall, bool isComboPaywall, bool isFeaturedToggle, string price, string markOutPrice = null)
	{
	}

	public void SetUAPLabel(string label)
	{
	}

	public void SetButtonSound(SOUND sound)
	{
	}

	public void Select()
	{
	}

	public void Unselect()
	{
	}

	public void SetOnClickDelegateMethodChoiceIndex(int index)
	{
	}

	public void SetOnClickDelegateMethod(UnityAction<int> callback)
	{
	}
}
