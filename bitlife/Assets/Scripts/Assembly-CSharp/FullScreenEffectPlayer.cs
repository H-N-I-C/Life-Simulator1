using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FullScreenEffectPlayer : MonoBehaviour
{
	private sealed class _003CTriggerEffect_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FullScreenEffectPlayer _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CTriggerEffect_003Ed__19(int _003C_003E1__state)
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

	private static FullScreenEffectPlayer _fullScreenEffectPlayer;

	private UnityEvent _onEffectBegin;

	private UnityEvent _onEffectEnd;

	private Canvas _canvasOverlay;

	private Texture2D _fullScreenImage;

	private Coroutine _runningEffectCoroutine;

	private IFullScreenEffectController _fullScreenEffectController;

	public static FullScreenEffectPlayer Instance
	{
		get
		{
			return null;
		}
		private set
		{
		}
	}

	public event UnityAction OnEffectBegin
	{
		add
		{
		}
		remove
		{
		}
	}

	public event UnityAction OnEffectEnd
	{
		add
		{
		}
		remove
		{
		}
	}

	private void Start()
	{
	}

	public void StartFullScreenEffect(IFullScreenEffectController fullScreenEffectController)
	{
	}

	public void StopFullScreenEffect()
	{
	}

	private IEnumerator TriggerEffect()
	{
		return null;
	}

	private void ResetEffect()
	{
	}
}
