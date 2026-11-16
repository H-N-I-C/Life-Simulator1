using CandyBusiness.Confidence.Actions;
using CandyBusiness.Confidence.Variants;
using Tracking;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyPromotion : UIDarkly
	{
		public const string TEMPLATE_ID_LARGE_PRIMARY_BUTTON_ONLY = "large_primary_button_only";

		public const string TEMPLATE_ID_MEDIUM_PRIMARY_BUTTON_ONLY = "medium_primary_button_only";

		public const string TEMPLATE_ID_LARGE_TWO_BUTTONS = "large_two_buttons";

		private PromotionalButtonOverlay _large_primary_button_only;

		private PromotionalButtonOverlay _medium_primary_button_only;

		private PromotionalButtonOverlay _large_two_buttons;

		private Transform _contentParent;

		private PromotionalButtonOverlay _currentOverlay;

		private ConfidenceVisualPromoTelemetryContext _telemetryContext;

		private bool _viewTracked;

		private bool _interactionTracked;

		public bool SetData(TriggerPromotionVariant data, ConfidenceVisualPromoTelemetryContext telemetryContext)
		{
			return false;
		}

		public void TrackViewed()
		{
		}

		public void TrackDismissal()
		{
		}

		private void CurrentOverlayOnOnButtonPressed(ConfidenceVisualPromoClickedOn clickedOn, ConfidenceAction actionToPerform)
		{
		}

		private void CurrentOverlayOnExitButtonPressed()
		{
		}

		private void TrackInteraction(ConfidenceVisualPromoClickedOn clickedOn)
		{
		}

		private void Exit()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
