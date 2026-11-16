using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandywriterPolicy
{
	public static class PrivacyPolicyExtentions
	{
		private sealed class _003CMorph_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public float startScale;

			public float endScale;

			public float duration;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CMorph_003Ed__9(int _003C_003E1__state)
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

		private sealed class _003CSlide_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public Vector2 startPos;

			public Vector2 endPos;

			public float duration;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSlide_003Ed__5(int _003C_003E1__state)
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

		private sealed class _003CSlide_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public Vector2 startPos;

			public Vector2 endPos;

			public float duration;

			public bool isLessThan;

			public Vector2 triggerPoint;

			public Action callback;

			private float _003CelapsedTime_003E5__2;

			private bool _003CusedCallback_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSlide_003Ed__6(int _003C_003E1__state)
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

		private static bool isInTransition;

		public static bool IsInTransit()
		{
			return false;
		}

		public static float GetT(float elapsedTime, float duration)
		{
			return 0f;
		}

		public static void MoveTo(RectTransform rect, Vector2 pos)
		{
		}

		public static void ScaleTo(RectTransform rect, float size)
		{
		}

		public static IEnumerator Slide(RectTransform rect, Vector2 startPos, Vector2 endPos, float duration)
		{
			return null;
		}

		public static IEnumerator Slide(RectTransform rect, Vector2 startPos, Vector2 endPos, float duration, bool isLessThan, Vector2 triggerPoint, Action callback)
		{
			return null;
		}

		public static void Move(RectTransform rect, Vector2 pos, Action callback)
		{
		}

		public static void Move(RectTransform rect, Vector2 pos)
		{
		}

		public static IEnumerator Morph(RectTransform rect, float startScale, float endScale, float duration)
		{
			return null;
		}

		public static void Scale(RectTransform rect, float scaleZeroToOne)
		{
		}
	}
}
