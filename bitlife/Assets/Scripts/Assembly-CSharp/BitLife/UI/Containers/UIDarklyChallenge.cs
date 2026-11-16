using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyChallenge : UIDarkly
	{
		private sealed class _003CStampAnimation_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChallenge _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStampAnimation_003Ed__42(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private sealed class _003CUpdateCountdownTimer_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChallenge _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CUpdateCountdownTimer_003Ed__43(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private RectTransform roundedRectContainer;

		private LayoutElement roundedRectContainerLayoutElement;

		private CandyImage roundedBackground;

		private CandyImage logo;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private RectTransform entry;

		private CandyImage entryTemplateBackground;

		private CandyText entryTemplateText;

		private CandyImage entryTemplateEmoji;

		private RectTransform shrinkableAreaRect;

		private VerticalLayoutGroup shrinkableAreaVerticalLayoutGroup;

		private RectTransform requirementContainer;

		private RectTransform completedContainer;

		private CandyText completedLabel;

		private CandyImage completedCheckbox;

		private GameObject statusBannerPlusSafeAreaBg;

		private GameObject statusBanner;

		private GameObject continueButtonSpacer;

		private CandyButton continueButton;

		private GameObject treasureChest;

		private CandyImage stamp;

		private GameObject stampSpacer;

		private GameObject starburst;

		private GameObject starburstTreasure;

		private CandyText challengeEndsLabel;

		private CandyText countdownLabel;

		private ChallengeMetadata challengeMetadata;

		private CompletedChallenge completedChallenge;

		private bool shouldOfferReward;

		private float containerMinHeight;

		private float stampSpace;

		private Coroutine updateTimer;

		public bool ShouldOfferReward => false;

		public void Setup(int challengeID)
		{
		}

		private int DevOverrideGlobalRank(int globalRank)
		{
			return 0;
		}

		private bool DevOverrideRanked(bool ranked)
		{
			return false;
		}

		private bool DevOverrideActive(bool active)
		{
			return false;
		}

		private bool DevOverrideChallengeCompleted(bool challengeCompleted)
		{
			return false;
		}

		private void DevOverrideShouldOfferReward()
		{
		}

		private IEnumerator StampAnimation()
		{
			return null;
		}

		private IEnumerator UpdateCountdownTimer()
		{
			return null;
		}

		private void UpdateCountdown()
		{
		}

		private void ContinueButtonPress()
		{
		}

		public Vector2 GetRoundedRectContainerSize()
		{
			return default;
		}

		public Vector2 GetRoundedRectPosition()
		{
			return default;
		}
	}
}
