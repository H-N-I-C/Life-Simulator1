using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacOSTTS : MonoBehaviour
{
	private sealed class _003CSpeakText_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSpeakText_003Ed__4(int _003C_003E1__state)
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

	public static MacOSTTS instance;

	private bool m_IsSpeaking;

	private void Start()
	{
	}

	public void Speak(string msg)
	{
	}

	private IEnumerator SpeakText(string textToSpeak)
	{
		return null;
	}

	public void Stop()
	{
	}

	public bool IsSpeaking()
	{
		return false;
	}

	private void OnDestroy()
	{
	}
}
