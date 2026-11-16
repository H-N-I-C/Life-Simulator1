using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyTimeMachine : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int> _003C_003E9__30_0;

			public static Action<int> _003C_003E9__31_0;

			public static Action<int> _003C_003E9__32_1;

			internal void _003CDoItCoinButtonPress_003Eb__30_0(int _)
			{
			}

			internal void _003CShowInsufficientCoinBalance_003Eb__31_0(int _)
			{
			}

			internal void _003CPurchaseCoinTrip_003Eb__32_1(int _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass30_0
		{
			public UIDarklyTimeMachine _003C_003E4__this;

			public GameManager game;

			public int years;

			public string lifeId;

			public int age;

			public Life destination;

			public int price;

			internal void _003CDoItCoinButtonPress_003Eb__1(int index)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass32_0
		{
			public GameManager game;

			public string lifeId;

			public int age;

			public int years;

			public Life destination;

			public UIDarklyTimeMachine _003C_003E4__this;

			internal void _003CPurchaseCoinTrip_003Eb__0(bool purchased)
			{
			}
		}

		private CandyImage roundedBackground;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandyButton yearButton;

		private Transform contentParent;

		private CandyButton moneyButton;

		private CandyButton unlimitedButton;

		private CandyButton coinButton;

		private ConstraintContentSizeFitter contentFitter;

		private CandyScroller scroller;

		private ConstraintContentSizeFitter scenarioStackFitter;

		private bool isInitialDarkly;

		private new Scenario scenario;

		private int currentlyChosenYearIndex;

		private List<CandyButton> yearButtons;

		private List<string> yearToGoBack;

		private List<string> ageToGoBack;

		private Texture2D check;

		private Texture2D nocheck;

		public void Setup(bool isInitialDarkly, Scenario mScenario)
		{
		}

		private void OnDestroy()
		{
		}

		public override void RebuildUI()
		{
		}

		public override void UpdateDarklyLayout()
		{
		}

		private void UpdateCoinButtonLayout()
		{
		}

		private void RefreshCoinButton()
		{
		}

		public static void ApplyMoneyButtonIconSize(CandyButton button)
		{
		}

		private static void ApplyCoinButtonArt(CandyButton coinButton, int price)
		{
		}

		private static void SetCoinButtonFirstLinePositions(UIDarklyButton buttonArt)
		{
		}

		private static void EnsurePriceCoin(Transform parent, Sprite sprite, float x)
		{
		}

		private void DoItCoinButtonPress()
		{
		}

		private static void ShowInsufficientCoinBalance()
		{
		}

		private void PurchaseCoinTrip(GameManager game, int years, string lifeId, int age, Life destination, int price)
		{
		}

		public void OnInteractCallback(AccessibleCandyButton button)
		{
		}

		public override int GetChildIndex(int siblingIndex)
		{
			return 0;
		}

		public override void ChildPress(int index)
		{
		}

		private void DoItButtonPress()
		{
		}

		private void UnlimitedButtonPress()
		{
		}

		private void DoItUnlimitedButtonPress()
		{
		}

		private int GetYears()
		{
			return 0;
		}
	}
}
