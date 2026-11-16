using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.SupportObjects
{
	public class UIStatsBarGraphs : MonoBehaviour, IguiObject
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<UIStatsBar> _003C_003E9__28_0;

			public static Action<UIStatsBar> _003C_003E9__29_0;

			internal void _003CHideBoosts_003Eb__28_0(UIStatsBar i)
			{
			}

			internal void _003CRemoveBoosts_003Eb__29_0(UIStatsBar i)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass27_0
		{
			public int attribute;

			internal bool _003CShowBoost_003Eb__0(UIStatsBar x)
			{
				return false;
			}
		}

		private RectTransform rect;

		private GameObject barGraphColumnParent;

		private CandyImage shadow;

		private int lifeEventTheme;

		private float barHeight;

		private float spacingBetweenBars;

		private float boostOverlayHeight;

		private float emojiPosX;

		private float warningPosX;

		private float statsPosX;

		private float Scale;

		private List<UIStatsBar> statBars;

		private float TitlePosX => 0f;

		private Vector2 EmojiSize => default;

		private bool IsVampire => false;

		public RectTransform Rect => null;

		private void Initialize(int numBars)
		{
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void UpdateStatsBarGraphs(UIStatsDataObject data)
		{
		}

		public void SetToGrayScale()
		{
		}

		public void TranslateBars()
		{
		}

		private void SetBarPosX(bool hasWarning)
		{
		}

		private Sprite GetEmoji(UIStatsBar bar)
		{
			return null;
		}

		public void ShowBoost(int attribute, ScenarioBoost boostScenario)
		{
		}

		public void HideBoosts()
		{
		}

		public void RemoveBoosts()
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetHeightConstraint()
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		internal static Sprite _003CGetEmoji_003Eg__Get_007C26_0(string emojiString)
		{
			return null;
		}
	}
}
