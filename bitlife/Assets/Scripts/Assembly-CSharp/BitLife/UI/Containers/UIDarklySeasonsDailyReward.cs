using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using Game.Seasons.Components;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklySeasonsDailyReward : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<DailyPendingRewardModel, int> _003C_003E9__69_0;

			internal int _003CGetDisplayedRewardAmount_003Eb__69_0(DailyPendingRewardModel reward)
			{
				return 0;
			}
		}

		private sealed class _003C_003Ec__DisplayClass87_0
		{
			public SeasonsDailyRewardModel data;

			public int displayedDay;
		}

		private sealed class _003C_003Ec__DisplayClass87_1
		{
			public int day;

			public int tier;

			public _003C_003Ec__DisplayClass87_0 CS_0024_003C_003E8__locals1;

			internal bool _003CSetData_003Eb__0(DailyPendingRewardModel reward)
			{
				return false;
			}
		}

		private sealed class _003CRevealGroups_003Ed__92 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float duration;

			public CanvasGroup[] groups;

			private float _003Celapsed_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealGroups_003Ed__92(int _003C_003E1__state)
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

		private sealed class _003CRevealPresentation_003Ed__89 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklySeasonsDailyReward _003C_003E4__this;

			private bool _003Creduced_003E5__2;

			private UIDailyRewardDayBadge[] _003C_003E7__wrap2;

			private int _003C_003E7__wrap3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealPresentation_003Ed__89(int _003C_003E1__state)
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

		private TextMeshProUGUI rewardText;

		private TextMeshProUGUI streakText;

		private TextMeshProUGUI balanceText;

		private TextMeshProUGUI todayAmount;

		private TextMeshProUGUI claimText;

		private TextMeshProUGUI pendingText;

		private TextMeshProUGUI storeText;

		private TextMeshProUGUI streakLabelText;

		private TextMeshProUGUI tomorrowIntroText;

		private TextMeshProUGUI nextRewardText;

		private TextMeshProUGUI catalogDateText;

		private TextMeshProUGUI progressStateText;

		private TextMeshProUGUI todayDayText;

		private TextMeshProUGUI tomorrowRewardText;

		private CandyButton claimButton;

		private CandyButton storeButton;

		private CandyButton closeButton;

		private Animator claimCheckAnimator;

		private Animator claimButtonTransitionAnimator;

		private GameObject claimedStamp;

		private GameObject pendingArea;

		private GameObject storePip;

		private UIDailyRewardDayBadge[] dayBadges;

		private Image nextRewardContour;

		private Image todayCardImage;

		private Image todayCoinImage;

		private Sprite nextRewardContourNormal;

		private Sprite nextRewardContourLast;

		private Sprite todayCardNormal;

		private Sprite todayCardClaimed;

		private Sprite todayCardBonus;

		private Sprite todayCoinNormal;

		private Sprite todayCoinClaimed;

		private Sprite todayCoinBonusClaimed;

		private GameObject bonusFlame;

		private GameObject bonusTierFlames;

		private GameObject bonusCardOuter;

		private GameObject todayClaimedBase;

		private GameObject tomorrowMessage;

		private CanvasGroup streakIconGroup;

		private CanvasGroup streakTextGroup;

		private CanvasGroup streakLabelGroup;

		private CanvasGroup todayGroup;

		private CanvasGroup claimGroup;

		private bool presentationReady;

		private bool presentationStarted;

		private bool canClaim;

		private bool claimAnimationInProgress;

		private bool claimCheckStarted;

		private SeasonsDailyRewardModel boundData;

		private Coroutine reveal;

		private CanvasGroup bonusCardOuterGroup;

		private CanvasGroup bonusTierFlamesGroup;

		private UIDailyRewardDayBadge bonusBadge;

		private DateTime nextRefreshUtc;

		private float nextCountdownUpdate;

		private int nextRewardTier;

		private AccessibleLabel overviewAccessibility;

		private AccessibleLabel countdownAccessibility;

		private bool presentationTimingStarted;

		private double presentationCallStartedAt;

		private double? apiCallStartedAt;

		public override bool ShouldTranslateUI => false;

		public void BeginPresentationTiming(double openingStartedAt, double? requestStartedAt)
		{
		}

		private void LogPresentationTiming(string phase)
		{
		}

		protected override void Awake()
		{
		}

		public static int GetNextRewardTier(SeasonsDailyRewardModel data)
		{
			return 0;
		}

		public static bool IsBonusState(SeasonsDailyRewardModel data)
		{
			return false;
		}

		public static int GetDisplayedRewardAmount(SeasonsDailyRewardModel data)
		{
			return 0;
		}

		public static string FormatCatalogDates(DailyMarketplaceModel marketplace)
		{
			return null;
		}

		private static string FormatCatalogDate(DateTime date)
		{
			return null;
		}

		private void RefreshNextRewardSelection()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Refresh()
		{
		}

		private void RefreshStorePip()
		{
		}

		private void Update()
		{
		}

		private void RefreshCountdown()
		{
		}

		public static string FormatNextRewardCountdown(TimeSpan remaining)
		{
			return null;
		}

		private void OpenStore()
		{
		}

		private void Close()
		{
		}

		private void Claim()
		{
		}

		private void ClaimFailed()
		{
		}

		private void ShowMultipleRewards(IReadOnlyList<DailyPendingRewardModel> rewards)
		{
		}

		private void FinishClaimPresentation()
		{
		}

		private void BeginClaimCheckAnimation()
		{
		}

		public void SetData(SeasonsDailyRewardModel data)
		{
		}

		public override void DoVirtualOverrideMethodForAfterTransitionOntoScreen()
		{
		}

		private IEnumerator RevealPresentation()
		{
			return null;
		}

		private void EnsureRevealDecorations()
		{
		}

		private static void AlignRevealPivot(CanvasGroup decoration, Component reward)
		{
		}

		private static IEnumerator RevealGroups(float duration, CanvasGroup[] groups)
		{
			return null;
		}

		private void CenterStreakRow()
		{
		}

		private void ApplyTodayGeometry(bool bonus)
		{
		}

		private static void SetDesignRect(RectTransform rect, float x, float y, float width, float height)
		{
		}

		private static void SetSingleLineValue(TextMeshProUGUI label, string value, float maximumSize, float minimumSize)
		{
		}

		private void _003CClaim_003Eb__82_0(int amount, List<DailyPendingRewardModel> rewards)
		{
		}
	}
}
