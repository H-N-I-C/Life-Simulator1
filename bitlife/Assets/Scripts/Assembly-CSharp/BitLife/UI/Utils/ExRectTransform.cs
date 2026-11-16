using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BitLife.UI.Utils
{
	public static class ExRectTransform
	{
		private sealed class _003CMorph_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public float startScale;

			public float endScale;

			public float duration;

			public CurveType curveType;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CMorph_003Ed__22(int _003C_003E1__state)
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

		private sealed class _003CMorphByAxis_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public float startScale;

			public float endScale;

			public float duration;

			public CurveType curveType;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CMorphByAxis_003Ed__23(int _003C_003E1__state)
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

		private sealed class _003CSlide_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public bool setIsInTransitFlag;

			public float duration;

			public Vector2 startPos;

			public Vector2 endPos;

			public bool isLinear;

			public CurveType curveType;

			public bool isOscillating;

			private OnDisableDispatcher _003Cdispatcher_003E5__2;

			private float _003CelapsedTime_003E5__3;

			private Vector2 _003CoscillationDisplacement_003E5__4;

			private Vector2 _003CMaxOscillationDisplacement_003E5__5;

			private float _003CdurationPerOscillation_003E5__6;

			private float _003ChalfDurationPerOscillation_003E5__7;

			private int _003CcurveTypeIndex_003E5__8;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSlide_003Ed__18(int _003C_003E1__state)
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

		private sealed class _003CSlide_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public Action callback;

			public Vector2 startPos;

			public Vector2 endPos;

			public float duration;

			public bool isLessThan;

			public Vector2 triggerPoint;

			private OnDisableDispatcher _003Cdispatcher_003E5__2;

			private float _003CelapsedTime_003E5__3;

			private bool _003CusedCallback_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSlide_003Ed__19(int _003C_003E1__state)
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

		private sealed class _003CTwirl_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float startRotationSpeed;

			public float endRotationSpeed;

			public float duration;

			public RectTransform rect;

			private float _003CelapsedTime_003E5__2;

			private Vector3 _003Caxis_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTwirl_003Ed__17(int _003C_003E1__state)
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

		private static float GetTotalHierarchialScaleRecursively(Transform transform, float scale)
		{
			return 0f;
		}

		public static float GetHeight(RectTransform rect)
		{
			return 0f;
		}

		public static void SetHeight(RectTransform rect, float height)
		{
		}

		public static void SetWidth(RectTransform rect, float width)
		{
		}

		public static void MoveTo(RectTransform rect, Vector2 pos)
		{
		}

		public static void ScaleTo(RectTransform rect, float size)
		{
		}

		public static void ScaleToInAxis(RectTransform rect, float size, int axis)
		{
		}

		public static bool IsStretchingHorizontally(RectTransform rect)
		{
			return false;
		}

		public static RectTransform SetHorizontalStretch(RectTransform rect)
		{
			return null;
		}

		public static RectTransform SetOffsetToZero(RectTransform rect, bool doleft = true, bool doright = true)
		{
			return null;
		}

		public static void SetLeft(RectTransform rt, float left)
		{
		}

		public static void SetRight(RectTransform rt, float right)
		{
		}

		public static void SetTop(RectTransform rt, float top)
		{
		}

		public static void SetBottom(RectTransform rt, float bottom)
		{
		}

		public static void DisableTransit(Transform transform)
		{
		}

		public static IEnumerator Twirl(RectTransform rect, float startRotationSpeed, float endRotationSpeed, float duration)
		{
			return null;
		}

		public static IEnumerator Slide(RectTransform rect, Vector2 startPos, Vector2 endPos, float duration, bool setIsInTransitFlag = true, bool isLinear = false, CurveType curveType = CurveType.Default, bool isOscillating = false)
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

		public static IEnumerator Morph(RectTransform rect, float startScale, float endScale, float duration, CurveType curveType = CurveType.Parametric)
		{
			return null;
		}

		public static IEnumerator MorphByAxis(RectTransform rect, int axis, float startScale, float endScale, float duration, CurveType curveType = CurveType.Parametric)
		{
			return null;
		}
	}
}
