using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyEmigrate : UIDarkly
	{
		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public DataCountry countryData;

			internal bool _003CChildPress_003Eb__0(SimPlace x)
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

		private List<Transform> buttonsForIndex;

		private Dictionary<int, int> childArrayIndexToSiblingIndex;

		private List<GameObject> contents;

		private void Reset()
		{
		}

		private void DestroyContents()
		{
		}

		public override void UpdateDarklyButtons()
		{
		}

		public void Setup()
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
