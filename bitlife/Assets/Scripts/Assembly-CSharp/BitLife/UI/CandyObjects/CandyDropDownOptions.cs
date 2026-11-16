using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace BitLife.UI.CandyObjects
{
	public class CandyDropDownOptions : IguiOptions
	{
		public float width;

		public CandyTextOptions titleOptions;

		public IguiOptions[] menuEntries;

		public GameObject[] menuEntryDisplayObjects;

		public Color primaryColor;

		public Color secondaryColor;

		public Color frameColor;

		public Color highlightColor;

		public CandyImageOptions dropDownArrowOptions;

		public bool displayShadow;

		public int revealedItemCount;

		public bool displayDropDownMenu;

		public UnityAction<int> delegateMethod;

		public int defaultSelectedIndexZI;

		public CandyDropDownOptions(float width, CandyTextOptions titleOptions, IguiOptions[] menuEntries, GameObject[] menuEntryDisplayObjects, Color primaryColor, Color secondaryColor, Color frameColor, Color highlightColor, CandyImageOptions dropDownArrowOptions, bool displayShadow, int revealedItemCount, bool displayDropDownMenu, UnityAction<int> delegateMethod, int defaultSelectedIndexZI)
		{
		}

		public Vector2 GetSize()
		{
			return default;
		}

		public bool HasTexture()
		{
			return false;
		}
	}
}
