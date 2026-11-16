using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyChildren : UIDarkly
	{
		private const float TITLE_TO_DETAILES_SPACE = 25f;

		private RectTransform stack;

		private CandyText childrenDetails;

		private Transform contentParent;

		private GameObject separator;

		private GameObject blank;

		private ConstraintContentSizeFitter sizeFitter;

		private CandyScroller scroller;

		private ArrayList childArray;

		private float targetSharedTitleContainerVerticalPosition;

		private SimChild willedChild;

		private string detailsText;

		private ArrayList childDarklyButtons;

		private List<int> childDarklyIndices;

		private Dictionary<int, int> childArrayIndexToSiblingIndex;

		private List<Transform> buttonsForIndex;

		public float GetTopOfRect()
		{
			return 0f;
		}

		public void Setup(ArrayList children)
		{
		}

		public override int GetChildIndex(int siblingIndex)
		{
			return 0;
		}

		public override void ChildPress(int index)
		{
		}
	}
}
