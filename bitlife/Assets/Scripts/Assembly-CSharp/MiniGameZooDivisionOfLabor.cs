using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameZooDivisionOfLabor : MonoBehaviour, IguiObject
{
	private sealed class _003CSelectAfterActivated_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameZooDivisionOfLabor _003C_003E4__this;

		public int selectedIndex;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSelectAfterActivated_003Ed__32(int _003C_003E1__state)
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

	public int NumberOfStaffAssigned;

	public int MaxStaff;

	private bool _initialized;

	private float _width;

	private float _listStartVertical;

	private Scenario _scenario;

	private MiniGameDataZooDivisionOfLabor _data;

	private GameObject _totalStaffLabelObject;

	private GameObject _totalStaffCountLabelObject;

	private int _totalStaff;

	private List<MiniGameZooDivisionOfLaborSlider> _items;

	private List<Slider> _itemsSlider;

	private MiniGameZooDivisionOfLaborSlider _slider;

	private bool _buttonJustPressed;

	private TextMeshProUGUI _totalStaffCountLabel;

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

	public void ButtonUp()
	{
	}

	public void ButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
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

	public void UpdateAssignedStaff()
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
