using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameSlots : MonoBehaviour, IguiObject
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<int, bool> _003C_003E9__48_0;

		internal bool _003CSetLightsActive_003Eb__48_0(int i)
		{
			return false;
		}
	}

	private sealed class _003CSpin_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameSlots _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSpin_003Ed__26(int _003C_003E1__state)
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

	private List<MiniGameSlotsCylinder> mCylindersList;

	private List<Image> mSlotMachineLightsList;

	private float BUTTON_GAP;

	private float SPIN_DURATION;

	private float SPIN_DURATION_SHIFT;

	private bool mButtonsEnabled;

	private bool mButtonJustPressed;

	private float mWidth;

	private Scenario mScenario;

	private SimCasinoSession mMiniGameData;

	private GameObject mSlotMachineView;

	private GameObject mResultLabel;

	private GameObject winLabel;

	private GameObject mSpinButton;

	private GameObject mPlayAgainButton;

	private GameObject mSpinFasterButton;

	private GameObject mTutorialButton;

	private MiniGameSlotsCombination mCurrentCombination;

	private bool mIsWin;

	private Coroutine mSpinCoroutine;

	private bool _isDone;

	private float _sessionWinnings;

	public static bool ForceTadPoles;

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	private IEnumerator Spin()
	{
		return null;
	}

	private void SpinFast()
	{
	}

	private void OnSpinDone()
	{
	}

	private void UpdateResultsLabel(bool isWin, float value)
	{
	}

	private void SetActiveSpinButton(GameObject spinButton)
	{
	}

	public void ButtonDownSpin()
	{
	}

	public void ButtonUpSpin()
	{
	}

	public void ButtonDownAgain()
	{
	}

	public void ButtonUpAgain()
	{
	}

	private void ResetGame()
	{
	}

	public void ButtonDownSpinFaster()
	{
	}

	public void ButtonUpSpinFaster()
	{
	}

	public void ButtonDownTutorial()
	{
	}

	public void ButtonUpTutorial()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private void ExitGame()
	{
	}

	private void ShowTutorial()
	{
	}

	private void ShowInsufficientFunds()
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

	private void SetLightsActive(bool active)
	{
	}

	private void StopLightsAnimation()
	{
	}

	private void PlayWinAnimation()
	{
	}

	private void _003CSpinFast_003Eb__27_0()
	{
	}

	private bool _003CSetLightsActive_003Eb__48_1(int i)
	{
		return false;
	}

	private void _003CPlayWinAnimation_003Eb__50_0()
	{
	}

	private void _003CPlayWinAnimation_003Eb__50_1()
	{
	}

	private void _003CPlayWinAnimation_003Eb__50_2()
	{
	}

	private void _003CPlayWinAnimation_003Eb__50_3()
	{
	}
}
