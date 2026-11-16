using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMP_LinkHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IScenarioComponentEvent
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__15_0;

		internal string _003CSetupAccessibility_003Eb__15_0(string linkId)
		{
			return null;
		}
	}

	private sealed class _003CPlayPressed_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TMP_LinkHandler _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CPlayPressed_003Ed__19(int _003C_003E1__state)
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

	private ScenarioComponentEvent ScenarioEventData__BackingField;

	public const string COPY_TO_CLIPBOARD = "copy-to-clipboard";

	private TextMeshProUGUI textMeshPro;

	private const string COPY_ICON_NORMAL = "copy-icon-normal";

	private const string COPY_ICON_PRESSED = "copy-icon-pressed";

	private const string COPY_ICON_PATH = "TMP Sprite Assets/copy-icons";

	private const float COPY_PRESSED_TIME = 1f;

	public ScenarioComponentEvent ScenarioEventData
	{
		get
		{
			return ScenarioEventData__BackingField;
		}
		private set
		{
			ScenarioEventData__BackingField = value;
		}
	}

	private bool IsCopyToClipboard => false;

	private void Awake()
	{
	}

	private void OnDisable()
	{
	}

	public void SetScenarioEventData(ScenarioComponentEvent data)
	{
	}

	private void SetupAccessibility()
	{
	}

	private void ActivateLink(string linkId)
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	private IEnumerator PlayPressed()
	{
		return null;
	}

	private void ReplaceText(string oldValue, string newValue)
	{
	}
}
