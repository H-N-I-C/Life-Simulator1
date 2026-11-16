using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyGoldenDiploma : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SimOccupationStudent, string> _003C_003E9__18_0;

			public static Func<string, string> _003C_003E9__18_2;

			internal string _003CSetup_003Eb__18_0(SimOccupationStudent x)
			{
				return null;
			}

			internal string _003CSetup_003Eb__18_2(string x)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public int i;

			internal bool _003CSetup_003Eb__1(SimOccupationStudent x)
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

		private List<string> degreeTitles;

		private string chosenDegree;

		private List<Transform> buttonsForIndex;

		private Dictionary<int, int> childArrayIndexToSiblingIndex;

		private List<GameObject> contents;

		private SCHOOL_MAJOR currentMajor;

		private new ScenarioGoldenDiploma scenario;

		private void Reset()
		{
		}

		private void DestroyContents()
		{
		}

		public void Setup(ScenarioGoldenDiploma scenario)
		{
		}

		public override int GetChildIndex(int siblingIndex)
		{
			return 0;
		}

		public override void ChildPress(int index)
		{
		}

		public string GetChosenDegree()
		{
			return null;
		}
	}
}
