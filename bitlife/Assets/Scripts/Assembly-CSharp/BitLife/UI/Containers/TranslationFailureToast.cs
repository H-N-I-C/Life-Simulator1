using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public sealed class TranslationFailureToast : MonoBehaviour
	{
		private sealed class _003CHideAfterDelay_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public TranslationFailureToast _003C_003E4__this;

			private float _003Celapsed_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHideAfterDelay_003Ed__40(int _003C_003E1__state)
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

		private const string ResourcePath = "TranslationFailureWarning";

		private const string Message = "Couldn't translate. Showing English for now.";

		private const float DesignUnit = 3.1846154f;

		private const float Width = 945.83075f;

		private const float Height = 98.723076f;

		private const float IconInset = 12.7384615f;

		private const float IconSize = 73.246155f;

		private const float FontSize = 35.03077f;

		private const float CapsuleOffset = 3.1846154f;

		private const float StrokeWidth = 6.3692307f;

		private const float CornerRadius = 66.87692f;

		private const float ShadowOffset = 6.3692307f;

		private const float TextLeft = 130.56923f;

		private const float TextRight = 57.32308f;

		private const float TextVerticalInset = 25.476923f;

		private const float Margin = 38.215385f;

		private const float GapBelowHeroBar = 31.846153f;

		private const float VisibleSeconds = 10f;

		private const float FadeSeconds = 0.25f;

		private static TranslationFailureToast instance;

		private readonly Vector3[] heroCorners;

		private RectTransform parentRect;

		private RectTransform heroBarRect;

		private RectTransform rect;

		private CanvasGroup canvasGroup;

		private TextMeshProUGUI label;

		private Image capsuleShadow;

		private Image capsuleBorder;

		private Image capsuleFill;

		private Coroutine hideRoutine;

		private float layoutWidth;

		public static bool IsVisible => false;

		public static void Show(RectTransform parent, RectTransform heroBar)
		{
		}

		private bool Initialize()
		{
			return false;
		}

		private static Image CreateCapsuleImage(string name, RectTransform parent, Sprite sprite, Color color)
		{
			return null;
		}

		private static void SetCornerRadius(Image image, float radius)
		{
		}

		private void Present(RectTransform parent, RectTransform heroBar)
		{
		}

		private void LateUpdate()
		{
		}

		private void UpdateLayout()
		{
		}

		private IEnumerator HideAfterDelay()
		{
			return null;
		}

		private void OnDisable()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
