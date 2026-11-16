using System;
using UnityEngine;

public class GenericPaywallContentSO : ScriptableObject
{
	[Serializable]
	public class PaywallContentItem
	{
		public Sprite Icon;

		public string Text;
	}

	public Sprite LogoImage;

	public Sprite BackgroundImage;

	public Color BorderColor;

	public Color OddColorEntryBackground;

	public Color EvenColorEntryBackground;

	public string Header;

	public string BottomText;

	public string PaywallAccessibilityDescription;

	public PaywallContentItem[] Content;
}
