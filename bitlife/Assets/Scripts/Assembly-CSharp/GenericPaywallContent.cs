using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class GenericPaywallContent : MonoBehaviour, IGenericPaywallContent
{
	private GenericPaywallContentLibrarySO paywallContentLibrary;

	private CandyText headerText;

	private CandyText bottomText;

	private CandyImage logoImage;

	private CandyImage backgroundImage;

	private CandyImage borderImage;

	private RectTransform contentContainer;

	private GenericPaywallContentEntry contentEntryTemplate;

	private GenericPaywallContentSO _genericPaywallContent;

	private List<GenericPaywallContentEntry> _contentEntries;

	private RectTransform _rectTransform;

	public void Setup(IAP_TYPE iapType)
	{
	}

	private void SetPaywallContent()
	{
	}

	private void FindIapContent(IAP_TYPE iapType)
	{
	}

	public void Rebuild()
	{
	}

	public string GetPaywallAccessibilityDescription()
	{
		return null;
	}

	private void SetContentHeight()
	{
	}
}
