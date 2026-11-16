using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Utils
{
	public static class ExCandyImage
	{
		private sealed class _003CFade_003Ed__0 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CandyImage candyImage;

			public Color startColor;

			public Color endColor;

			public float fadeDuration;

			public bool turnOffWhenDone;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFade_003Ed__0(int _003C_003E1__state)
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

		public static IEnumerator Fade(CandyImage candyImage, float fadeDuration, Color startColor, Color endColor, bool turnOffWhenDone = false)
		{
			return null;
		}
	}
}
