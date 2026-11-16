using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyAppearance : UIDarkly
	{
		private sealed class _003CSelectElementAfterDelay_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyAppearance _003C_003E4__this;

			public int index;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSelectElementAfterDelay_003Ed__50(int _003C_003E1__state)
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

		private const float SCROLL_SLIDE_DURATION = 0.25f;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandyScroller featureScroller;

		private Transform featureScrollerContentParent;

		private CandyButton featureButtonTemplate;

		private CandyButton saveButton;

		private CandyButton left;

		private CandyButton right;

		private CandyScroller colorScroller;

		private Transform colorScrollerContentParent;

		private CandyButton colorButtonTemplate;

		private CandyImage blackBox;

		private GameObject colorScrollerSpacer;

		private GodModePersonConfig personConfig;

		private List<CandyButton> featureButtons;

		private List<CandyButton> colorButtons;

		private int currentColorIndex;

		private int currentFeatureIndex;

		private Color highlightColor;

		private UIDarklyAppearanceVoltron voltronFace;

		private bool appearanceSaved;

		private bool firstRead;

		private void OnDisable()
		{
		}

		public void Setup(GodModePersonConfig personConfig)
		{
		}

		private void SaveButtonPress()
		{
		}

		public override GodModePersonConfig GetPersonConfig()
		{
			return null;
		}

		public bool GetAppearanceSaved()
		{
			return false;
		}

		public void UpdateSetup(GodModePersonConfig personConfig)
		{
		}

		public void UpdateStyleButtons(bool shouldShow)
		{
		}

		public void UpdateFaceAndColorPicker()
		{
		}

		public void Randomize()
		{
		}

		public void BackButtonProcessing(bool shouldClearEmojiScript)
		{
		}

		private void LeftButtonPress()
		{
		}

		private void RightButtonPress()
		{
		}

		private void UpdateFeatures(ArrayList features)
		{
		}

		private void DestroyFeatureButtons()
		{
		}

		private int DetermineNearestFeature()
		{
			return 0;
		}

		private void GoToNearestFeature()
		{
		}

		private void FeatureIndexedButtonPressed(int index, bool updateFace = true)
		{
		}

		private void HighlightNearestFeature(int index = -1)
		{
		}

		private void UpdateFeatureScroller(int index)
		{
		}

		private void UpdateColors(List<Color> colors)
		{
		}

		private void DestroyColorButtons()
		{
		}

		private int DetermineNearestColor()
		{
			return 0;
		}

		private void GoToNearestColor()
		{
		}

		private void ColorIndexedButtonPressed(int index, bool updateFace = true)
		{
		}

		private void UpdateColorScroller(int index)
		{
		}

		private void HighlightNearestColor(int index = -1)
		{
		}

		private IEnumerator SelectElementAfterDelay(int index)
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

		public override int GetChildIndexMenu2(int siblingIndex)
		{
			return 0;
		}

		public override void ChildPressMenu2(int index)
		{
		}
	}
}
