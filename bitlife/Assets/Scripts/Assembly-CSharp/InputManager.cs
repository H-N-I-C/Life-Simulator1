using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private sealed class _003CClickTimer_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InputManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CClickTimer_003Ed__26(int _003C_003E1__state)
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

	private sealed class _003CClickTimerNES_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InputManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CClickTimerNES_003Ed__28(int _003C_003E1__state)
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

	public static InputManager SharedInputManager;

	private CandyText debugLog;

	private Vector2 lastInputPos;

	private float timeElapsedSinceLastClick;

	private float clickWaitTime;

	private bool pointerClicked;

	private bool pointerReleased;

	private Action ResetSpecificButton;

	private float timeElapsedSinceLastClickNES;

	private float clickWaitTimeNES;

	public Vector2 LastInputPos => default;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnBannerAdClicked()
	{
	}

	public void ResetButtons()
	{
	}

	public bool IsPointerReleased()
	{
		return false;
	}

	public void Release()
	{
	}

	public void UnClickNES()
	{
	}

	public bool ReadyToClickNES()
	{
		return false;
	}

	public bool IsPointerClicked()
	{
		return false;
	}

	public void UnClick()
	{
	}

	public void Click(Action ResetSpecificButton)
	{
	}

	public bool ReadyToClick()
	{
		return false;
	}

	public void StartClickTimer()
	{
	}

	private IEnumerator ClickTimer()
	{
		return null;
	}

	public void StartClickTimerNES()
	{
	}

	private IEnumerator ClickTimerNES()
	{
		return null;
	}
}
