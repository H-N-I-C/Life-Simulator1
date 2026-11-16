using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlertView : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public string title;

		public List<TranslationEntry> entries;

		public string message;

		public string cancelButtonTitle;

		public UIAlertView _003C_003E4__this;

		public Action<int> callback;

		public bool useCallbackOnCancel;

		public string[] otherButtonTitles;

		internal void _003CCreationProcess_003Eb__0(List<string> result)
		{
		}
	}

	private sealed class _003CSetupWindow_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIAlertView _003C_003E4__this;

		public string title;

		public string message;

		public string cancelButtonTitle;

		public string[] otherButtonTitles;

		public Action<int> callback;

		public bool useCallbackOnCancel;

		public bool translate;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSetupWindow_003Ed__18(int _003C_003E1__state)
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

	private const float START_SCALE = 2f;

	private const float END_SCALE = 0.9f;

	private const float END_SCALE_TABLET = 0.65f;

	private static bool IsModal__BackingField;

	public static UIAlertView Inst;

	private UIAlertViewWindow windowTemplate;

	private UIAlertViewWindow windowTemplateDarkMode;

	private UIAlertViewWindow window;

	private UIFadeImage shade;

	public static bool IsModal
	{
		get
		{
			return IsModal__BackingField;
		}
		set
		{
			IsModal__BackingField = value;
		}
	}

	public UIAlertViewWindow Window => null;

	private void Awake()
	{
	}

	public void Show(string title, string message, string cancelButtonTitle = null, string[] otherButtonTitles = null, Action<int> callback = null, bool useCallbackOnCancel = true, bool translate = true)
	{
	}

	public void ShowModal(string title, string message, string cancelButtonTitle = null, string[] otherButtonTitles = null, Action<int> callback = null, bool useCallbackOnCancel = true, bool translate = true)
	{
	}

	private void CreationProcess(string title, string message, string cancelButtonTitle = null, string[] otherButtonTitles = null, Action<int> callback = null, bool useCallbackOnCancel = true, bool translate = true)
	{
	}

	private IEnumerator SetupWindow(string title, string message, string cancelButtonTitle, Action<int> callback, bool useCallbackOnCancel, string[] otherButtonTitles, bool translate = true)
	{
		return null;
	}

	public void Remove(int okIndex = 0)
	{
	}

	private void KillIt()
	{
	}

	public static string Bold(string text)
	{
		return null;
	}
}
