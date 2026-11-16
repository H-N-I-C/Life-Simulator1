using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyCustomLifeSubGroup : UIDarkly
	{
		private sealed class _003C_003Ec__DisplayClass24_0
		{
			public DataCountry country;

			internal bool _003CSetup_003Eb__0(string x)
			{
				return false;
			}
		}

		private CandyImage titleEmoji;

		private CandyText details;

		private Transform contentParent;

		private CandyButton darklyButton;

		private GameObject separator;

		private GameObject endSpace;

		private ConstraintContentSizeFitter fitter;

		private RectTransform titleContainer;

		private CandyScroller scroller;

		private List<string> items;

		private int chosenGender;

		private string chosenCountryName;

		private string chosenCity;

		private int chosenRoyalRank;

		private int chosenSpecialTalent;

		private List<Transform> buttonsForIndex;

		private Dictionary<int, int> childArrayIndexToSiblingIndex;

		private List<GameObject> contents;

		public new virtual bool ShouldTranslateUI => false;

		private void Reset()
		{
		}

		private void DestroyContents()
		{
		}

		public override void UpdateDarklyLayout()
		{
		}

		public override void UpdateDarklyButtons()
		{
		}

		public void Setup(int subGroupType, DataCountry country, int gender, string city)
		{
		}

		private string EmojiStringForRoyalStatusForIndex(int count, DataCountry country)
		{
			return null;
		}

		public override int GetChildIndex(int siblingIndex)
		{
			return 0;
		}

		public override void ChildPress(int index)
		{
		}

		private void ShowCustomCityScenario()
		{
		}

		public override int GetSubGroupType()
		{
			return 0;
		}

		public int GetChosenGender()
		{
			return 0;
		}

		public string GetChosenCountryName()
		{
			return null;
		}

		public string GetChosenCity()
		{
			return null;
		}

		public int GetChosenRoyalRank()
		{
			return 0;
		}

		public int GetChosenSpecialTalent()
		{
			return 0;
		}
	}
}
