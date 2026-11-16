using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public static class ExColor
{
	private sealed class _003C_003Ec__DisplayClass2_0
	{
		public float value;

		public float saturation;

		public float hue;

		internal float _003CGetDescriptiveColorName_003Eb__0(float a)
		{
			return 0f;
		}

		internal float _003CGetDescriptiveColorName_003Eb__1(float a)
		{
			return 0f;
		}

		internal float _003CGetDescriptiveColorName_003Eb__2(float a)
		{
			return 0f;
		}
	}

	private sealed class _003CTransition_003Ed__1 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CandyImage image;

		public Color startColor;

		public Color endColor;

		public float duration;

		public bool isLit;

		public float lightingAngle;

		private float _003CelapsedTime_003E5__2;

		private RectTransform _003CimageRect_003E5__3;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CTransition_003Ed__1(int _003C_003E1__state)
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

	public static string ColorToHex(Color color)
	{
		return null;
	}

	public static IEnumerator Transition(CandyImage image, Color startColor, Color endColor, float duration, bool isLit = false, float lightingAngle = 0f)
	{
		return null;
	}

	public static string GetDescriptiveColorName(Color color)
	{
		return null;
	}
}
