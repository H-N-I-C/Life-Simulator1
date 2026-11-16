using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public sealed class UIDailyRewardMultipleRewardsPopup : MonoBehaviour
	{
		private sealed class _003CScrollToBottom_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDailyRewardMultipleRewardsPopup _003C_003E4__this;

			private float _003Celapsed_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CScrollToBottom_003Ed__18(int _003C_003E1__state)
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

		private const string ResourcePath = "daily-reward/DailyRewardMultipleRewardsPopup";

		private const float AutoScrollSeconds = 1f;

		private const int VisibleRowCount = 2;

		private ScrollRect rewardScroll;

		private RectTransform rewardContent;

		private GameObject rewardRowTemplate;

		private CandyButton gotItButton;

		private CandyButton closeButton;

		private TextMeshProUGUI descriptionText;

		private static UIDailyRewardMultipleRewardsPopup activePopup;

		private Action dismissed;

		private Coroutine autoScroll;

		private bool isAutomaticRecovery;

		public static bool Show(IReadOnlyList<DailyPendingRewardModel> rewards, Action onDismiss, bool automaticRecovery = false)
		{
			return false;
		}

		public static void CancelAutomaticRecovery()
		{
		}

		public static string RewardLabel(DailyPendingRewardModel reward)
		{
			return null;
		}

		private void Awake()
		{
		}

		private void Present(IReadOnlyList<DailyPendingRewardModel> rewards)
		{
		}

		private IEnumerator ScrollToBottom()
		{
			return null;
		}

		private void GotIt()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
