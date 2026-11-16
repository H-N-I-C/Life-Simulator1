using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyChallengeReward : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int> _003C_003E9__29_0;

			internal void _003CContinueButtonPress_003Eb__29_0(int i)
			{
			}
		}

		private sealed class _003CSelectionAnimation_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int index;

			public UIDarklyChallengeReward _003C_003E4__this;

			private DataAccessory _003Caccessory_003E5__2;

			private Sprite _003CaccessorySprite_003E5__3;

			private float _003CscaleFactor_003E5__4;

			private float _003CaccessoryPresentAnimationTime_003E5__5;

			private Vector2 _003CendPos_003E5__6;

			private Sprite _003CbadgeSprite_003E5__7;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSelectionAnimation_003Ed__34(int _003C_003E1__state)
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

		private sealed class _003CStartRewardGameAnimation_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChallengeReward _003C_003E4__this;

			private float _003Ccount_003E5__2;

			private Vector2[] _003Ccorners_003E5__3;

			private List<(int, int)>.Enumerator _003C_003E7__wrap3;

			private (int, int) _003CchestPair_003E5__5;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStartRewardGameAnimation_003Ed__31(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			void IEnumerator.Reset()
			{
			}
		}

		private static ChallengeRewardState _state;

		private RectTransform roundedRectContainer;

		private LayoutElement roundedRectContainerLayoutElement;

		private CandyImage roundedBackground;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandyImage animatedBackgroundChest;

		private CandyImage accessoryPrize;

		private CandyImage animatedForegroundChest;

		private CandyImage fireworks;

		private CandyImage starburst;

		private CandyText accessoryNameLabel;

		private CandyText accessoryColorLabel;

		private CandyImage badge;

		private GameObject continueButtonStandInSpacer;

		private GameObject continueButtonSpacer;

		private CandyButton continueButton;

		private RectTransform chestSetsParent_CornerPositions;

		private List<RectTransform> chestSets;

		private List<CandyImage> glows;

		private List<CandyImage> chests;

		private List<CandyButton> buttons;

		private CandyButton extraRewardButton;

		private List<(int, int)> desiredSequence;

		private bool _videoAvailable;

		private void OnDisable()
		{
		}

		public void Setup(Vector2 roundedRectSize)
		{
		}

		private void SetupDesiredSequence()
		{
		}

		private void ContinueButtonPress()
		{
		}

		private void ExtraButtonPress()
		{
		}

		private IEnumerator StartRewardGameAnimation()
		{
			return null;
		}

		public override void DoVirtualOverrideMethodForAfterTransitionOntoScreen()
		{
		}

		private void ChestButtonPressed(int index)
		{
		}

		private IEnumerator SelectionAnimation(int index)
		{
			return null;
		}

		public static void SetNewRewardState(ChallengeRewardState state)
		{
		}
	}
}
