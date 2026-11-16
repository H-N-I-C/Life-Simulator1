using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIRightButton : MonoBehaviour
	{
		private sealed class _003CHideAnimation_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public UIRightButton _003C_003E4__this;

			public float width;

			public float animDuration;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHideAnimation_003Ed__26(int _003C_003E1__state)
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

		private sealed class _003CShowAnimation_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public UIRightButton _003C_003E4__this;

			public float animTime;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowAnimation_003Ed__24(int _003C_003E1__state)
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

		public const float MARKETPLACE_BUTTON_WIDTH = 360f;

		public const float BLACKFRIDAY_BUTTON_WIDTH = 460f;

		public const float STPATRICT_BUTTON_WIDTH = 530f;

		public const float MARKETPLACE_BUTTON_WIDTH_SALE = 494f;

		public const float BITPASS_BUTTON_WIDTH_SALE = 470f;

		private RectTransform rect;

		private CandyButton button;

		private CandyImage background;

		private CandyImage icon;

		private CandyText title;

		private bool isOnScreen;

		private bool _hudActive;

		private int contentType;

		private Coroutine hide;

		private Coroutine show;

		private bool _keepUpdating;

		public static float GetWidthByType(RIGHT_SIDE_BUTTON_TYPE buttonType)
		{
			return 0f;
		}

		private void OnDisable()
		{
		}

		public void Setup(int contentType)
		{
		}

		private void ButtonPress()
		{
		}

		private AccessibleCandyButton UapButton()
		{
			return null;
		}

		private void RefreshUapAccessibility()
		{
		}

		public void SetHudActive(bool hudActive)
		{
		}

		public void Show(float delay = 0f, float animTime = 0.35f)
		{
		}

		private IEnumerator ShowAnimation(float delay = 0f, float animTime = 0.35f)
		{
			return null;
		}

		public void Hide(float width, float delay = 0f, float animDuration = 0.35f)
		{
		}

		private IEnumerator HideAnimation(float width, float delay = 0f, float animDuration = 0.35f)
		{
			return null;
		}

		public bool IsOnScreen()
		{
			return false;
		}

		public void UpdateScreen()
		{
		}

		public void UpdateScreenForTime(float seconds)
		{
		}

		private void EndUpdateTime()
		{
		}

		private void Update()
		{
		}
	}
}
