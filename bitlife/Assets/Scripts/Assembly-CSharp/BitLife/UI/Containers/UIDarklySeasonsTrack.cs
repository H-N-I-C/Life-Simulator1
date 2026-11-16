using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using Game.Seasons;
using Game.Seasons.Components;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklySeasonsTrack : UIDarkly
	{
		public enum UpdateType
		{
			None = 0,
			ScrollToPosition = 1,
			ScrollAnimatedToPosition = 2,
			HighlightFrameOnly = 3
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<UISeasonsTrackItemGroup> _003C_003E9__16_0;

			public static Predicate<UISeasonsTrackItemGroup> _003C_003E9__21_0;

			public static Predicate<UISeasonsTrackItemGroup> _003C_003E9__21_1;

			internal void _003COnTrackItemRedeemed_003Eb__16_0(UISeasonsTrackItemGroup i)
			{
			}

			internal bool _003CUpdateHighlightedFramePosition_003Eb__21_0(UISeasonsTrackItemGroup i)
			{
				return false;
			}

			internal bool _003CUpdateHighlightedFramePosition_003Eb__21_1(UISeasonsTrackItemGroup i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public UIDarklySeasonsTrack _003C_003E4__this;

			public SeasonModel data;

			internal void _003CSetData_003Eb__0(int index, UISeasonsTrackItemGroup item, TrackItemModel trackData)
			{
			}
		}

		private sealed class _003CAnimateSetStartingScrollPosition_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float time;

			public ScrollRect scrollRect;

			public float endPos;

			private float _003Ctick_003E5__2;

			private Vector2 _003CstartPos_003E5__3;

			private Vector2 _003Cpos_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CAnimateSetStartingScrollPosition_003Ed__22(int _003C_003E1__state)
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

		private sealed class _003CSetData_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklySeasonsTrack _003C_003E4__this;

			public SeasonModel data;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSetData_003Ed__19(int _003C_003E1__state)
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

		private static UIDarklySeasonsTrack _instance;

		private TextMeshProUGUI textTier;

		private TextMeshProUGUI textTierProgress;

		private TextMeshProUGUI textSeasonTimeLeft;

		private UISeasonsTrackItemGroup itemTemplate;

		private Button buttonBattlePass;

		private Button buttonInfo;

		private GameObject stack;

		private UpdateType postRedeemUpdateType;

		private List<UISeasonsTrackItemGroup> items;

		private bool _coolDown;

		private CandyScroller scroller;

		protected override void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnTrackItemRedeemed(SeasonRedeemModel data)
		{
		}

		private void OnSeasonTimeChanged(SeasonTimeInfo info)
		{
		}

		public void RefreshData()
		{
		}

		public IEnumerator SetData(SeasonModel data)
		{
			return null;
		}

		private void UpdateScrollPosition(bool initialize = false)
		{
		}

		private int UpdateHighlightedFramePosition()
		{
			return 0;
		}

		private IEnumerator AnimateSetStartingScrollPosition(ScrollRect scrollRect, float endPos, float time = 0.5f)
		{
			return null;
		}

		private void BattlePassClick()
		{
		}

		private void ClickInfoPanel()
		{
		}

		private void ShowProgress()
		{
		}

		public static bool IsCoolDown()
		{
			return false;
		}

		public static void ActivateCoolDown()
		{
		}

		private void MonoCoolDown()
		{
		}

		private void DeactivateCoolDown()
		{
		}

		public static void Exit()
		{
		}
	}
}
