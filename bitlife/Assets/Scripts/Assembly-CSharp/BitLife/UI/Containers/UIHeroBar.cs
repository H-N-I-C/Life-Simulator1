using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Data;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIHeroBar : MonoBehaviour
	{
		private sealed class _003CSetBankBalanceValueOverTime_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIHeroBar _003C_003E4__this;

			private double _003CfinalValue_003E5__2;

			private double _003Crate_003E5__3;

			private double _003CcurrentValue_003E5__4;

			private bool _003CvalueIsIncreasing_003E5__5;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSetBankBalanceValueOverTime_003Ed__53(int _003C_003E1__state)
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

		private RectTransform rect;

		private CandyImage background;

		private CandyImage topBorder;

		private CandyImage bottomBorder;

		private CandyImage heroImage;

		private CandyImage prisonBars;

		private CandyImage occupationImage;

		private CandyText occupationText;

		private CandyImage flagBackerImage;

		private CandyImage flagImage;

		private CandyText bankBalanceValue;

		private ConstraintLayoutElementByTMPro bankValueConstraint;

		private CandyText bankBalanceLabel;

		private CandyText heroName;

		private RectTransform heroNameRect;

		private CandyImage infoImage;

		private ConstraintToRectEdge infoConstraint;

		private RectTransform nameInfoBankGroup;

		private CandyButton heroBarButton;

		private GameObject lifeEventOverlayParent;

		private CandyImage prison;

		private CandyImage politics;

		private CandyImage sports;

		private CandyImage streetHustler;

		private CandyImage royalty;

		private CandyImage fame;

		private CandyImage music;

		private CandyImage mafia;

		private CandyImage actor;

		private CandyImage business;

		private CandyImage astronaut;

		private CandyImage dealer;

		private CandyImage vampire;

		private LIFE_EVENT_THEME_NAME currentLifeEvent;

		private Coroutine bankRoll;

		private double currentBankBalanceForCalculations;

		public void Init()
		{
		}

		public void SetLifeEventTheme(LIFE_EVENT_THEME_NAME lifeEventTheme)
		{
		}

		public void LoadLifeEventThemes()
		{
		}

		public static LIFE_EVENT_THEME_NAME GetLifeEventTheme()
		{
			return LIFE_EVENT_THEME_NAME.NONE;
		}

		private void UpdateTheme()
		{
		}

		public void UpdateHeroBarTheme()
		{
		}

		public void UpdateOccupationString(string text)
		{
		}

		public void UpdateContent(string heroName, string occupation, string bankValue, Color bankColor, string heroCountryCode, Sprite occupationImage)
		{
		}

		public void TranslateHeroBar()
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetHeroBarButtonOnClickMethod(Action delegateMethod)
		{
		}

		private void SetOccupationImage(Texture2D tex)
		{
		}

		private void SetFlagImage(Texture2D tex)
		{
		}

		private void SetHeroImage(Texture2D tex)
		{
		}

		public void ResetBankBalanceValue()
		{
		}

		private void SetBankBalanceValue(string value)
		{
		}

		private void SetBankBalanceValueInternal(string value)
		{
		}

		private IEnumerator SetBankBalanceValueOverTime()
		{
			return null;
		}

		private void SetHeroName(string value)
		{
		}

		public void HeroBarSwap()
		{
		}
	}
}
