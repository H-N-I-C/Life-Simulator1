using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BitLife.UI.Utils
{
	public class FadeCandyText : MonoBehaviour
	{
		private sealed class _003CFade_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public FadeCandyText _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFade_003Ed__7(int _003C_003E1__state)
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

		private sealed class _003CFadeOutAnimation_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public FadeCandyText _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFadeOutAnimation_003Ed__9(int _003C_003E1__state)
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

		private TextMeshProUGUI tmp;

		private Color startColor;

		private Color endColor;

		private float fadeDuration;

		private float fadeOutDuration;

		private float elapsedTime;

		private void OnEnable()
		{
		}

		private IEnumerator Fade()
		{
			return null;
		}

		public void FadeOut(float delay = 0f)
		{
		}

		private IEnumerator FadeOutAnimation(float delay = 0f)
		{
			return null;
		}

		private void SetColor(Color color)
		{
		}
	}
}
