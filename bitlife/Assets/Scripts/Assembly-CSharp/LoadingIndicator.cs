using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
	private sealed class _003CStopLoadingAfterDelay_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public LoadingIndicator _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CStopLoadingAfterDelay_003Ed__16(int _003C_003E1__state)
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

	public static LoadingIndicator Inst;

	private CandyImage background;

	private CandyImage loadingImage;

	private int frameCount;

	private float duration;

	private CandyText label;

	private float timeElapsed;

	private int currentFrame;

	private bool _lockLoader;

	private void OnDisable()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	public void StartWithString(string message)
	{
	}

	public void StartWithString(string message, bool lockLoader)
	{
	}

	public void StopLoading(float delay = 0f)
	{
	}

	public void StopLoading(bool lockLoader)
	{
	}

	private IEnumerator StopLoadingAfterDelay(float delay)
	{
		return null;
	}

	private void Update()
	{
	}

	private void MoveForwardByFrameCount(int numberOfFramesToMoveForwardBy)
	{
	}
}
