using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UIDailyRewardDayBadge : MonoBehaviour
	{
		private sealed class _003CRevealAnimated_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float duration;

			public UIDailyRewardDayBadge _003C_003E4__this;

			public CanvasGroup decoration;

			private float _003Celapsed_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealAnimated_003Ed__36(int _003C_003E1__state)
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

		private CanvasGroup canvasGroup;

		private TextMeshProUGUI dayLabel;

		private TextMeshProUGUI amountLabel;

		private TextMeshProUGUI nextLabel;

		private GameObject claimedStamp;

		private GameObject nextRibbon;

		private AccessibleLabel accessibleLabel;

		private float baseScale;

		private Image cardImage;

		private Image coinImage;

		private GameObject flame;

		private Sprite normalCard;

		private Sprite lastCard;

		private Sprite claimedCard;

		private Sprite normalCoin;

		private Sprite claimedCoin;

		private Sprite bonusCoin;

		private Sprite dimmedNormalCoin;

		private Sprite dimmedClaimedCoin;

		private Sprite normalStamp;

		private Sprite dimmedStamp;

		private Color dimmedDayBackdrop;

		private Color dimmedAmountBackdrop;

		private Material normalDayMaterial;

		private Material dimmedDayMaterial;

		private Material normalAmountMaterial;

		private Material dimmedAmountMaterial;

		private float visibleAlpha;

		public void SetData(int day, int amount, int currentDay, bool rewardedToday, bool isPending = false)
		{
		}

		public static string FormatStreakLabel(int day)
		{
			return null;
		}

		public void SetWindowData(int day, int amount, int currentDay, bool rewardedToday, bool isPending, bool highlighted, bool dimmed, bool rolling)
		{
		}

		public void SetBonusData(int streakCount, int amount, bool claimed)
		{
		}

		private AccessibleLabel EnsureAccessibleLabel()
		{
			return null;
		}

		private void ApplyVisualState(bool lastDay, bool claimed, bool dimmed = false)
		{
		}

		public void Reveal(CanvasGroup decoration = null)
		{
		}

		public void PrepareReveal()
		{
		}

		public IEnumerator RevealAnimated(float duration, CanvasGroup decoration = null)
		{
			return null;
		}
	}
}
