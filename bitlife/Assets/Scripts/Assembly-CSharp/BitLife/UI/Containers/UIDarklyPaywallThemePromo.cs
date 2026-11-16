using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallThemePromo : UIDarkly
	{
		private sealed class _003CShineRevealAnimation_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyPaywallThemePromo _003C_003E4__this;

			private Vector2 _003CfirstShineStart_003E5__2;

			private Vector2 _003CfirstShineEnd_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShineRevealAnimation_003Ed__23(int _003C_003E1__state)
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

		private const float FIRST_SHINE_START_POS_X = -685f;

		private const float FIRST_SHINE_END_POS_X = 1055f;

		private const float FIRST_SHINE_POS_Y = -78f;

		private const float FIRST_SHINE_START_SCALE_X = 2f;

		private const float FIRST_SHINE_END_SCALE_X = 0.3f;

		private const float FIRST_SHINE_ANIM_DURATION = 1f;

		private const float GOLD_MASK_START_POS_X = 385f;

		private const float GOLD_MASK_END_POS_X = 1705f;

		private const float GOLD_MASK_POS_Y = -711f;

		private const float GOLD_LAYER_END_POS_X = -617f;

		private const float GOLD_LAYER_END_POS_Y = 12.9f;

		private Scenario followUpScenario;

		private Scenario followUpChainedScenario;

		private CandyText details;

		private CandyButton orBuyItNowButton;

		private RectTransform firstShine;

		private RectTransform secondShine;

		private RectTransform goldMask;

		private RectTransform goldLayer;

		private bool isInitialDarkly;

		private int IAP_Origin;

		private void OnDisable()
		{
		}

		public void Setup(bool isInitialDarkly, int IAP_Origin, Scenario scenario)
		{
		}

		private IEnumerator ShineRevealAnimation()
		{
			return null;
		}

		private void OrBuyItNowButtonPress()
		{
		}

		private void ExitPaywall()
		{
		}

		public void LaunchFollowUpScenario()
		{
		}
	}
}
