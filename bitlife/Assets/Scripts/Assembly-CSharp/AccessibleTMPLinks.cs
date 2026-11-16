using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccessibleTMPLinks : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass15_0
	{
		public AccessibleTMPLinks _003C_003E4__this;

		public string capturedId;

		internal void _003CCreateOverlay_003Eb__0()
		{
		}
	}

	private sealed class _003CBuildWhenReady_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AccessibleTMPLinks _003C_003E4__this;

		private int _003Cattempt_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CBuildWhenReady_003Ed__12(int _003C_003E1__state)
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

	private TextMeshProUGUI _text;

	private readonly List<GameObject> _overlays;

	private Func<string, string> _getLabel;

	private Action<string> _onActivate;

	private Coroutine _buildRoutine;

	private TextMeshProUGUI Text => null;

	public void Rebuild(Func<string, string> getLabel, Action<string> onActivate)
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void RestartBuild()
	{
	}

	private void StopBuild()
	{
	}

	private IEnumerator BuildWhenReady()
	{
		return null;
	}

	private bool TryBuildOverlays()
	{
		return false;
	}

	private bool TryGetLinkBounds(TMP_TextInfo textInfo, TMP_LinkInfo link, out Vector2 center, out Vector2 size)
	{
		center = default;
		size = default;
		return false;
	}

	private GameObject CreateOverlay(string linkId, string linkText, Vector2 center, Vector2 size)
	{
		return null;
	}

	private void ClearOverlays()
	{
	}
}
