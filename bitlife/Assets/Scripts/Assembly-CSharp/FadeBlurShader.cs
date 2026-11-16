using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlurShader : MonoBehaviour
{
	private sealed class _003CFadeInAnimation_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FadeBlurShader _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CFadeInAnimation_003Ed__15(int _003C_003E1__state)
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

	private sealed class _003CFadeOutAnimation_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public FadeBlurShader _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CFadeOutAnimation_003Ed__17(int _003C_003E1__state)
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

	private Material blurMaterial;

	private float blurStartValue;

	private float blurEndValue;

	private float multiplyTintStartValue;

	private float multiplyTintEndValue;

	private float delayDuration;

	private float fadeDuration;

	private float fadeOutDuration;

	private float elapsedTime;

	private bool isFadingInOnEnable;

	private bool resetOnDisable;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public float GetFadeOutDuration()
	{
		return 0f;
	}

	public void FadeIn()
	{
	}

	public IEnumerator FadeInAnimation()
	{
		return null;
	}

	public void FadeOut(float delay = 0f)
	{
	}

	public IEnumerator FadeOutAnimation(float delay = 0f)
	{
		return null;
	}

	private void SetBlurValue(float value)
	{
	}

	private void SetMultValue(float value)
	{
	}
}
