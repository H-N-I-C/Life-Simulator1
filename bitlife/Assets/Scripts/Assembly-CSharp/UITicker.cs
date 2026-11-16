using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class UITicker : MonoBehaviour
{
	private sealed class _003CRemoveAnimation_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool animated;

		public UITicker _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CRemoveAnimation_003Ed__23(int _003C_003E1__state)
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

	private sealed class _003CShowAnimation_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UITicker _003C_003E4__this;

		public Vector2 startPos;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CShowAnimation_003Ed__21(int _003C_003E1__state)
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

	private sealed class _003CUpdateTimer_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UITicker _003C_003E4__this;

		public float scrollDur;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CUpdateTimer_003Ed__30(int _003C_003E1__state)
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

	private const float ANIM_DUR = 0.5f;

	private const float SCROLL_DUR = 20f;

	private const float UPDATE_TIME = 0.2f;

	private CandyImage background;

	private CandyImage sperm;

	private CandyImage bottomBorder;

	private CandyText message;

	private CandyButton challengeButton;

	private RectTransform messageContainer;

	private RectTransform rect;

	private int contentType;

	private Coroutine scrollAnim;

	private Coroutine updateTimer;

	private float timeRemaining;

	private bool isScrolling;

	private bool isOnPause;

	private bool isSetup;

	private void OnDisable()
	{
	}

	private void Start()
	{
	}

	public void Setup(int contentType)
	{
	}

	public void Show()
	{
	}

	private IEnumerator ShowAnimation(Vector2 startPos)
	{
		return null;
	}

	public void Remove(bool animated = true)
	{
	}

	private IEnumerator RemoveAnimation(bool animated)
	{
		return null;
	}

	private void RemoveScrollAnim()
	{
	}

	public void Pause()
	{
	}

	public void Resume()
	{
	}

	private string GetMessageText(int contentType)
	{
		return null;
	}

	private void ChallengeButtonPressed()
	{
	}

	private void ScrollMessage(float duration)
	{
	}

	private IEnumerator UpdateTimer(float scrollDur)
	{
		return null;
	}

	private bool UpdateMessage()
	{
		return false;
	}
}
