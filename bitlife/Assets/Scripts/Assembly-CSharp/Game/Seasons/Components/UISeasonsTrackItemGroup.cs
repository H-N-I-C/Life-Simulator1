using System.Collections.Generic;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UISeasonsTrackItemGroup : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public TrackItemModel itemData;

			internal bool _003CSetData_003Eb__0(SeasonRedeemModel i)
			{
				return false;
			}
		}

		private Toggle toggleOrdinal;

		private TextMeshProUGUI textOrdinal;

		private List<UISeasonsTrackItem> items;

		private Image imageTrackRowDivider;

		private Image imageTrackRowDividerActive;

		private Image imageDivider;

		private Image imageDividerActive;

		private Image imageDividerActiveBot;

		private bool IsUnlockable__BackingField;

		private bool IsRedeemable__BackingField;

		private bool HasBitsRequired__BackingField;

		public bool IsUnlockable
		{
			get
			{
				return IsUnlockable__BackingField;
			}
			private set
			{
				IsUnlockable__BackingField = value;
			}
		}

		public bool IsRedeemable
		{
			get
			{
				return IsRedeemable__BackingField;
			}
			private set
			{
				IsRedeemable__BackingField = value;
			}
		}

		public bool HasBitsRequired
		{
			get
			{
				return HasBitsRequired__BackingField;
			}
			private set
			{
				HasBitsRequired__BackingField = value;
			}
		}

		public RectTransform rectform => null;

		public void SetData(int index, ScrollRect parentScroll, SeasonModel data, TrackItemModel itemData)
		{
		}

		public void ShowDivider(bool normal, bool active)
		{
		}

		public void RefreshData()
		{
		}
	}
}
