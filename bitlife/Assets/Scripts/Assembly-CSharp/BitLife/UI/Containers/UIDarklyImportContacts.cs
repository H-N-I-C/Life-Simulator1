using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyImportContacts : UIDarkly
	{
		private CandyImage roundedBackground;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandyButton nameButton;

		private Transform contentParent;

		private CandyButton saveButton;

		private ConstraintContentSizeFitter contentFitter;

		private CandyScroller scroller;

		private ConstraintContentSizeFitter scenarioStackFitter;

		private Texture2D check;

		private Texture2D nocheck;

		private List<CandyButton> nameButtons;

		private List<bool> selectedItems;

		public override bool ShouldTranslateUI => false;

		public void Setup()
		{
		}

		private void IndexedButtonPress(int i)
		{
		}

		private void DoItButtonPress()
		{
		}

		private CustomPerson CreateCustomPerson(string inputFirstName, string inputLastName)
		{
			return null;
		}
	}
}
