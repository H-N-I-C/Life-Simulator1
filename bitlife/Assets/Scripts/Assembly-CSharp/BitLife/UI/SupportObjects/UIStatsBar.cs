using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.SupportObjects
{
	public class UIStatsBar : MonoBehaviour, IguiObject
	{
		private sealed class _003CRemoveBoostOverlayAnimation_003Ed__47 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIStatsBar _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveBoostOverlayAnimation_003Ed__47(int _003C_003E1__state)
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

		private sealed class _003CRevealBoostOverlayAnimation_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIStatsBar _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealBoostOverlayAnimation_003Ed__46(int _003C_003E1__state)
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

		private const float DURATION_A = 0.2625f;

		private const float DURATION_B = 0.075f;

		private const float DURATION_C = 0.075f;

		private const float DURATION_REMOVE = 0f;

		private RectTransform rect;

		private Image image;

		private Image valueImage;

		private CandyText percentage;

		private Image percentageImageParent;

		private RectTransform boostOverlayContainer;

		private RectTransform animationGroup;

		private CandyImage boostOverlayTail;

		private CandyButton boostButton;

		private CandyImage emoji;

		private CandyText title;

		private CandyImage themeOverlay;

		private RectTransform titleRect;

		private RectTransform warningRect;

		private AccessibleCandyButton UAPComponent;

		private AccessibleLabel UAPLabel;

		public int boostAttribute;

		private int Index__BackingField;

		private float startPosX;

		private float endPosX;

		private bool boostIsOn;

		private Color red_170_35_24;

		private int colorScheme;

		private float value;

		public int Index
		{
			get
			{
				return Index__BackingField;
			}
			private set
			{
				Index__BackingField = value;
			}
		}

		public float Value
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		private void OnDisable()
		{
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public UIStatsBar Initialize(int index, float titlePosX, float barHeight, Transform barGraphColumnParent, RectTransform barGraphColumnRect, float boostOverlayHeight, float warningPosX, Sprite emojiSprite, Vector2 emojiSize, float emojiPosX)
		{
			return null;
		}

		public UIStatsBar Refresh(ScenarioBarGraph barGraph, float warningPosX)
		{
			return null;
		}

		private void DebugSetNameByAttribute(BOOST_ATTRIBUTE attribute)
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public string GetTitle()
		{
			return null;
		}

		public float GetFillWidth()
		{
			return 0f;
		}

		public void SetThemeOverlay()
		{
		}

		public void SetToGrayScale()
		{
		}

		public void ShowBoost(ScenarioBoost boostScenario)
		{
		}

		public void RemoveBoostOverlay(bool forceImmediate = false)
		{
		}

		private IEnumerator RevealBoostOverlayAnimation()
		{
			return null;
		}

		private IEnumerator RemoveBoostOverlayAnimation()
		{
			return null;
		}

		private void RemoveBoostOverlay()
		{
		}

		public void SetBoostOverlayTailImage(Texture2D texture)
		{
		}

		public void SetBoostOverlayContainerHeight(int value)
		{
		}

		public void SetWarningAttributes(float posX)
		{
		}

		public void SetWarningVisibility(bool forceOff = false)
		{
		}

		public void SetWarningPos(float posX)
		{
		}

		public bool IsWarningVisible()
		{
			return false;
		}

		public void SetTitleAttributes(float posX)
		{
		}

		public void SetTitleColor(bool forceNormal = false)
		{
		}

		public void SetTitleTranslated()
		{
		}

		public void SetEmojiSprite(Sprite sprite)
		{
		}

		public void SetColorScheme(int theColorScheme)
		{
		}

		public void SetPercentageFontSize(int value)
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		public void SetHeight(float height)
		{
		}

		public void SetBackgroundColor(Color color)
		{
		}

		public void UpdateValueBarColor()
		{
		}

		public void SetHeightConstraint()
		{
		}
	}
}
