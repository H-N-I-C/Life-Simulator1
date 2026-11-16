using System;
using BitLife.UI.CandyObjects;
using BitLife.UI.Managers;
using BitLife.UI.ScenarioObjects;
using Game.Seasons.Components;
using Game.Seasons.Models;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyStats : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int> _003C_003E9__40_0;

			internal void _003CTimeMachineButtonPress_003Eb__40_0(int _)
			{
			}
		}

		private const float TABLE_VERTICAL_INSET = 0f;

		private const float TABLE_HORIZONTAL_INSET = 60f;

		private UIScenarioManager scenarioManager;

		private CandyImage shadeBackground;

		private CandyImage statsBackground;

		private CandyImage sash;

		private CandyButton shareButton;

		private CandyText statsName;

		private CandyText date;

		private CandyScroller scroller;

		private CandyTable table;

		private GameObject tableToObitSpace;

		private CandyText obituary;

		private GameObject obitToBarSpace;

		private UIScenarioBarGraphs barGraphs;

		private CandyImage grass;

		private RectTransform contentRect;

		private CandyButton continueButton;

		private CandyButton timeMachineButton;

		private CandyButton timeMachineButtonTextBased;

		private GameObject claimItem;

		private CandyButton claimItemButton;

		public Grave data;

		private bool dailyRewardLifeEligible;

		private bool dailyRewardClaimInFlight;

		private bool dailyRewardStateInFlight;

		private bool dailyRewardExitInProgress;

		private bool dailyRewardCurrentLife;

		private string dailyRewardLifeId;

		private UIDailyLifeRewardButton dailyRewardButton;

		private Color fontColor;

		private bool DailyRewardNavigationBlocked => false;

		public bool CanPresentDailyReward => false;

		public void SetupGravestone(Vector2 size, Grave data, bool wasLaunchedFromMainMenu, bool doesIncludeJournal, bool doesIncludeBulldozer)
		{
		}

		private void SetupButtons()
		{
		}

		private static void ShowTimeMachineEntry(GameObject iconButton, GameObject textButton, bool available, bool hasUsedTimeMachine)
		{
		}

		private void ContinueButtonPress()
		{
		}

		private void ContinueAfterReward()
		{
		}

		private void TimeMachineButtonPress()
		{
		}

		private void Update()
		{
		}

		private void SetupHeight()
		{
		}

		public void SetupDeathLog(Grave data)
		{
		}

		private void SetupBarGraphs(Grave data)
		{
		}

		private void SetupBarGraphsFromScratch(Grave data)
		{
		}

		private void SetupObituary(Grave data)
		{
		}

		private void SetupTable(Grave data)
		{
		}

		public void OpenJournal()
		{
		}

		public void ExitGrave()
		{
		}

		public void Share()
		{
		}

		public void Bulldoze()
		{
		}

		private void ClaimReward()
		{
		}

		private void ShowLifeRewardFailure()
		{
		}

		private void RefreshDailyRewardClaim()
		{
		}

		private void OnDestroy()
		{
		}

		private void _003CContinueButtonPress_003Eb__34_0(bool success)
		{
		}

		private void _003CClaimReward_003Eb__52_0(DailyApiResult result)
		{
		}

		private void _003CClaimReward_003Eg__Complete_007C52_1()
		{
		}

		private void _003CShowLifeRewardFailure_003Eb__53_0(int index)
		{
		}
	}
}
