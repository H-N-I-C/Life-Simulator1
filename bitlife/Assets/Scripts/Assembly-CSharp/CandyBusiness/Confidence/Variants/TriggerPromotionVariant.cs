using System.Collections.Generic;
using CandyBusiness.Confidence.Actions;
using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;
using Tracking;
using UnityEngine;

namespace CandyBusiness.Confidence.Variants
{
	public class TriggerPromotionVariant : ConfidenceVariant
	{
		private sealed class _003C_003Ec__DisplayClass48_0
		{
			public string triggerValue;

			public TriggerPromotionVariant _003C_003E4__this;

			internal bool _003CShowQueuedTriggers_003Eb__0(string t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass48_1
		{
			public string promotionKey;

			public _003C_003Ec__DisplayClass48_0 CS_0024_003C_003E8__locals1;

			internal void _003CShowQueuedTriggers_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass50_0
		{
			public string seen;

			internal bool _003CClearViewPromotionViews_003Eb__0(string item)
			{
				return false;
			}
		}

		private const string CONFIDENCE_VIEW_PREFIX = "CONFIDENCE_PROMOTION_";

		private ConfidenceProperty<string> PromoID__BackingField;

		private ConfidenceProperty<int> MaxViews__BackingField;

		private ConfidenceProperty<string> Trigger__BackingField;

		private ConfidenceProperty<string> TemplateId__BackingField;

		private readonly ConfidenceProperty<string> ArtId__BackingField;

		private readonly ConfidenceProperty<string> PrimaryButtonLink__BackingField;

		private readonly ConfidenceProperty<string> SecondaryButtonLink__BackingField;

		private Texture2D Art__BackingField;

		private ConfidenceAction PrimaryButtonAction__BackingField;

		private ConfidenceAction SecondaryButtonAction__BackingField;

		private readonly List<string> _triggersQueued;

		private bool _gameOnboardFinished;

		private bool _presentationCheckScheduled;

		private bool _artIsBase64Payload;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<string> PromoID
		{
			get
			{
				return PromoID__BackingField;
			}
			private set
			{
				PromoID__BackingField = value;
			}
		}

		public ConfidenceProperty<int> MaxViews
		{
			get
			{
				return MaxViews__BackingField;
			}
			private set
			{
				MaxViews__BackingField = value;
			}
		}

		public ConfidenceProperty<string> Trigger
		{
			get
			{
				return Trigger__BackingField;
			}
			private set
			{
				Trigger__BackingField = value;
			}
		}

		public ConfidenceProperty<string> TemplateId
		{
			get
			{
				return TemplateId__BackingField;
			}
			private set
			{
				TemplateId__BackingField = value;
			}
		}

		public ConfidenceProperty<string> ArtId => ArtId__BackingField;

		public ConfidenceProperty<string> PrimaryButtonLink => PrimaryButtonLink__BackingField;

		public ConfidenceProperty<string> SecondaryButtonLink => SecondaryButtonLink__BackingField;

		public Texture2D Art
		{
			get
			{
				return Art__BackingField;
			}
			private set
			{
				Art__BackingField = value;
			}
		}

		public ConfidenceAction PrimaryButtonAction
		{
			get
			{
				return PrimaryButtonAction__BackingField;
			}
			private set
			{
				PrimaryButtonAction__BackingField = value;
			}
		}

		public ConfidenceAction SecondaryButtonAction
		{
			get
			{
				return SecondaryButtonAction__BackingField;
			}
			private set
			{
				SecondaryButtonAction__BackingField = value;
			}
		}

		private void OnOnboardingFinished()
		{
		}

		public override void Update()
		{
		}

		public void Show(string triggerId)
		{
		}

		internal void ShowQueuedTriggers()
		{
		}

		private void RetryQueuedTriggers()
		{
		}

		private void ClearViewPromotionViews()
		{
		}

		private int GetPromotionViewCount()
		{
			return 0;
		}

		private void LogPromotionViewed(string key)
		{
		}

		private string GeneratePromotionKey()
		{
			return null;
		}

		private bool TryCreateTelemetryContext(string triggerId, out ConfidenceVisualPromoTelemetryContext telemetryContext)
		{
			telemetryContext = null;
			return false;
		}
	}
}
