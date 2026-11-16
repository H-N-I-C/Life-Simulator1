using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BitLife.UI.Utils
{
	public class ConstraintScaleBgUI : MonoBehaviour
	{
		private sealed class _003CCheckForSafeAreaChange_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ConstraintScaleBgUI _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCheckForSafeAreaChange_003Ed__18(int _003C_003E1__state)
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

		public static ConstraintScaleBgUI Inst;

		public readonly float BASE_WIDTH;

		public readonly float BASE_HEIGHT;

		private const float TABLET_THRESHOLD = 1.47f;

		private Canvas canvas;

		private RectTransform rect;

		private RectTransform safeRect;

		private List<RectTransform> scaleNodes;

		private float scale;

		private bool isOnTablet;

		private Rect safeArea;

		private float timeElapsed;

		private readonly float timeAllowed;

		private Coroutine appling;

		public float Scale => 0f;

		private void Awake()
		{
		}

		public void Init()
		{
		}

		private IEnumerator CheckForSafeAreaChange()
		{
			return null;
		}

		public float GetWidth()
		{
			return 0f;
		}

		public float GetScale()
		{
			return 0f;
		}

		public float GetHeight()
		{
			return 0f;
		}

		public float GetCanvasHeight()
		{
			return 0f;
		}

		public float GetSafeHeightDifference()
		{
			return 0f;
		}

		public Vector2 GetSafeSize()
		{
			return default;
		}

		public float GetScaledSafeHeightDifference()
		{
			return 0f;
		}

		public float GetTopSafeAreaHeight()
		{
			return 0f;
		}

		public float GetScaledTopSafeAreaHeight()
		{
			return 0f;
		}

		public float GetBottomSafeAreaHeight()
		{
			return 0f;
		}

		public float GetScaledBottomSafeAreaHeight()
		{
			return 0f;
		}

		public float GetScaledSafeAreaHeight()
		{
			return 0f;
		}

		public float GetScaledSafeHeightPlusBottomNotch()
		{
			return 0f;
		}

		public bool IsOnTablet()
		{
			return false;
		}

		private void ApplySafeArea()
		{
		}

		private void UpdateConstraint()
		{
		}
	}
}
