using System.Collections.Generic;
using CandyBusiness.Confidence.Actions;
using CandyBusiness.Confidence.Variants;
using Tracking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromotionalButtonOverlay : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public PromotionalButtonOverlay _003C_003E4__this;

		public ConfidenceVisualPromoClickedOn clickedOn;

		public ConfidenceAction action;

		internal void _003CSetupButton_003Eb__0()
		{
		}
	}

	private UnityAction m_OnExitButtonPressed;

	private UnityAction<ConfidenceVisualPromoClickedOn, ConfidenceAction> m_OnButtonPressed;

	private List<Button> _buttonsOrdered;

	private Button _exitButton;

	private RawImage _backgroundArt;

	public event UnityAction OnExitButtonPressed
	{
		add
		{
		}
		remove
		{
		}
	}

	public event UnityAction<ConfidenceVisualPromoClickedOn, ConfidenceAction> OnButtonPressed
	{
		add
		{
		}
		remove
		{
		}
	}

	public void Setup(TriggerPromotionVariant promotionVariant)
	{
	}

	private void SetupButton(int buttonIndex, ConfidenceAction action, ConfidenceVisualPromoClickedOn clickedOn)
	{
	}

	private void _003CSetup_003Eb__9_0()
	{
	}
}
