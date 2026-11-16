using System.Collections.Generic;
using BitLife.UI.Interfaces;
using BitLife.UI.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandyDropDown : MonoBehaviour, IguiObject
	{
		private sealed class _003C_003Ec__DisplayClass17_0
		{
			public GameObject entryGameObject;

			public CandyDropDown _003C_003E4__this;

			internal void _003CConstructor_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass17_1
		{
			public GameObject entryGameObject;

			public CandyDropDown _003C_003E4__this;

			internal void _003CConstructor_003Eb__1()
			{
			}
		}

		private RectTransform rect;

		private ConstraintContentSizeFitter constraintContentSizeFitter;

		private CandyText title;

		private Image primaryBoxFrame;

		private CandyDropDownEntry primaryBoxEntry;

		private CandyImage primaryBoxBackground;

		private CandyImage dropDownArrow;

		private CandyScroller menu;

		private CandyImage menuBackground;

		private CandyButton fullScreenMenuToggleButton;

		private GameObject menuShadow;

		private GameObject contentParent;

		private GameObject dropDownEntry;

		private CandyImage overlay;

		private List<CandyDropDownEntry> dropDownEntries;

		private UnityAction<int> delegateMethod;

		private Color highlightColor;

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void OnDropDownSelection(int value)
		{
		}

		public string GetCurrentSelection()
		{
			return null;
		}

		public string GetCurrentSelectionOriginal()
		{
			return null;
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetHeightConstraint()
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		public void SetWidth(float width)
		{
		}

		public void ToggleMenu()
		{
		}
	}
}
