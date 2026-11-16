using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyEditPersonSubGroup : UIDarkly
	{
		private CandyImage titleEmoji;

		private CandyText details;

		private Transform contentParent;

		private CandyButton darklyButton;

		private GameObject separator;

		private GameObject endSpace;

		private ConstraintContentSizeFitter fitter;

		private RectTransform titleContainer;

		private CandyScroller scroller;

		private List<string> subGroupItems;

		private int chosenSpecialTalent;

		private List<Transform> buttonsForIndex;

		private Dictionary<int, int> childArrayIndexToSiblingIndex;

		private List<GameObject> contents;

		private GodModePersonConfig personConfig;

		private void Reset()
		{
		}

		private void DestroyContents()
		{
		}

		public override void UpdateDarklyButtons()
		{
		}

		public void Setup(GodModePersonConfig personConfig)
		{
		}

		public override int GetChildIndex(int siblingIndex)
		{
			return 0;
		}

		public override void ChildPress(int index)
		{
		}

		public int GetChosenSpecialTalent()
		{
			return 0;
		}
	}
}
