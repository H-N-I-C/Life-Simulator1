using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintSafeArea : MonoBehaviour
{
	private sealed class _003CCheckForSafeAreaChange_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ConstraintSafeArea _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CCheckForSafeAreaChange_003Ed__11(int _003C_003E1__state)
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

	private RectTransform rect;

	private Vector2 size;

	private Vector2 anchorMax;

	private Vector2 screenSize;

	private Rect safeArea;

	private float timeElapsed;

	private float timeAllowed;

	private Coroutine appling;

	public Vector2 Size => default;

	public void Init()
	{
	}

	private IEnumerator CheckForSafeAreaChange()
	{
		return null;
	}

	public float GetTopSafeAreaHeight()
	{
		return 0f;
	}

	public float GetBottomSafeAreaHeight()
	{
		return 0f;
	}

	private void ApplySafeArea()
	{
	}
}
