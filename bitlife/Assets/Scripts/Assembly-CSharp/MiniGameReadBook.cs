using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameReadBook : MonoBehaviour, IguiObject
{
	private sealed class _003CAnimateBook_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameReadBook _003C_003E4__this;

		private int _003Cx_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CAnimateBook_003Ed__21(int _003C_003E1__state)
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

	public static ButtonActivatedDelegate buttonActivatedDelegate;

	private float mWidth;

	private bool mInitialized;

	private bool mGameOver;

	private Scenario mScenario;

	private MiniGameDataBook mMiniGameData;

	private bool mButtonJustPressed;

	private int mCurrentPage;

	private GameObject mButtonObject;

	private GameObject mTextObject;

	private ScenarioMinigameTextRegistry mTextRegistry;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	public void ButtonUpBook()
	{
	}

	public void ButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private void TurnPage()
	{
	}

	private IEnumerator AnimateBook()
	{
		return null;
	}

	private void ExitGame()
	{
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}
}
