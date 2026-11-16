using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Utils
{
	public class FadeCandyImage : MonoBehaviour
	{
		private sealed class _003CFadeInAnimation_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public FadeCandyImage _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFadeInAnimation_003Ed__11(int _003C_003E1__state)
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

		private sealed class _003CFadeOutAnimation_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public FadeCandyImage _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFadeOutAnimation_003Ed__14(int _003C_003E1__state)
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

		private Image image;

		private Color startColor;

		private Color endColor;

		private float fadeDuration;

		private float fadeOutDuration;

		private float elapsedTime;

		private bool isFadingInOnEnable;

		public float FadeOutDuration => 0f;

		private void OnEnable()
		{
		}

		public void FadeIn()
		{
		}

		private IEnumerator FadeInAnimation()
		{
			return null;
		}

		public void SetFadeOutDuration(float fadeOutDuration)
		{
		}

		public void FadeOut(float delay = 0f)
		{
		}

		public IEnumerator FadeOutAnimation(float delay = 0f)
		{
			return null;
		}

		private void SetColor(Color color)
		{
		}
	}
}
