using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIAchievementBanner : MonoBehaviour
	{
		private sealed class _003CAnimationSequence_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIAchievementBanner _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CAnimationSequence_003Ed__13(int _003C_003E1__state)
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

		private const float POS_ABOVE_SCREEN = 600f;

		private const float EMOJI_TO_TITLE_GAP = 45f;

		private const float MARGINS = 90f;

		private const float ANIM_DURATION = 0.35f;

		private const float PAUSE_DURATION = 1.75f;

		private RectTransform rect;

		private CandyImage background;

		private RectTransform container;

		private CandyImage emoji;

		private CandyText title;

		private CandyText description;

		private void OnDisable()
		{
		}

		public void ShowBanner(string achievement, int type)
		{
		}

		private IEnumerator AnimationSequence()
		{
			return null;
		}
	}
}
