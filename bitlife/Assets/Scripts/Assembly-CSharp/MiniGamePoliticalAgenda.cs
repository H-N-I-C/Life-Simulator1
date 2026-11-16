using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class MiniGamePoliticalAgenda : MonoBehaviour, IguiObject
{
	private sealed class _003CSelectAfterActivated_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGamePoliticalAgenda _003C_003E4__this;

		public int selectedIndex;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSelectAfterActivated_003Ed__30(int _003C_003E1__state)
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

	public int mNumberOfHoursAssigned;

	public int mMaxHours;

	private bool mInitialized;

	private bool mButtonJustPressed;

	private float mWidth;

	private float mListStartVertical;

	private Scenario mScenario;

	private MiniGameDataPoliticalAgenda mData;

	private GameObject mTotalHoursLabelObject;

	private GameObject mTotalHoursCountLabelObject;

	private int mTotalHours;

	private List<MiniGamePoliticalAgendaIssue> mItems;

	private List<Slider> mItemsSlider;

	private MiniGamePoliticalAgendaIssue mTouchedItem;

	private bool mTouchInPlay;

	private ScenarioMinigameTextRegistry mTextRegistry;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	public void DoIt()
	{
	}

	public void GameOver()
	{
	}

	public void ExitGame()
	{
	}

	public float PositionList()
	{
		return 0f;
	}

	public void UpdateAssignedHours()
	{
	}

	public void FlashWarning()
	{
	}

	public void Flash1()
	{
	}

	public void Flash2()
	{
	}

	public void HandleOnClick(MiniGamePoliticalAgendaIssue issue)
	{
	}

	private IEnumerator SelectAfterActivated(int selectedIndex)
	{
		return null;
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
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
