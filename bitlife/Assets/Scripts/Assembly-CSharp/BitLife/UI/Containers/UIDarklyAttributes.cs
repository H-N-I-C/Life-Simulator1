using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyAttributes : UIDarkly
	{
		private CandyImage roundedBackground;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandySlider attributeSlider;

		private Transform contentParent;

		private CandyButton saveButton;

		private ConstraintContentSizeFitter contentFitter;

		private CandyScroller scroller;

		private ConstraintContentSizeFitter scenarioStackFitter;

		private List<CandySlider> sliders;

		private GodModePersonConfig personConfig;

		private Dictionary<int, int> sliderSiblingIndexToKeyID;

		private bool attributesSaved;

		private bool edited;

		private void DestroyAll()
		{
		}

		public void Setup(GodModePersonConfig personConfig)
		{
		}

		public void UpdateSetup(GodModePersonConfig personConfig)
		{
		}

		private string SexualityOverrideForValue(float value)
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

		public override void ChildSlide(float value, int index)
		{
		}

		public override GodModePersonConfig GetPersonConfig()
		{
			return null;
		}

		private void SaveButtonPress()
		{
		}

		public bool GetAttributesSaved()
		{
			return false;
		}
	}
}
