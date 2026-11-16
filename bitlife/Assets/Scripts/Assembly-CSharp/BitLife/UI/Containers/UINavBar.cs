using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using Game.Seasons.Components;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UINavBar : MonoBehaviour
	{
		private enum ObjectiveType
		{
			ActiveChallenge = 0
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<bool, bool> _003C_003E9__73_0;

			internal bool _003CSetupObjectiveButtonForActiveChallenge_003Eb__73_0(bool x)
			{
				return false;
			}
		}

		private sealed class _003CChallengeVaultButtonFade_003Ed__84 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UINavBar _003C_003E4__this;

			public float endValue;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CChallengeVaultButtonFade_003Ed__84(int _003C_003E1__state)
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

		private sealed class _003CRemoveObjectiveButtonAnimation_003Ed__83 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UINavBar _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveObjectiveButtonAnimation_003Ed__83(int _003C_003E1__state)
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

		private sealed class _003CRevealObjectiveButtonAnimation_003Ed__80 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UINavBar _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealObjectiveButtonAnimation_003Ed__80(int _003C_003E1__state)
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

		private const float HEIGHT_O = 166f;

		private const float HEIGHT = 107f;

		private const float ACHIEVEMENT_POS_IS_BITIZEN = -229f;

		private const float CHALLENGE_POS_IS_BITIZEN = -224f;

		private const float CHALLENGE_REVEAL_DURATION = 0.25f;

		private const float CHALLENGE_POS_TOP = 400f;

		private int challengeRequirementCount;

		private int challengeCompletedCount;

		private RectTransform rect;

		private CandyImage safeZoneBackground;

		private CandyImage background;

		private CandyImage backgroundOverlay;

		private CandyButton mainMenuButton;

		private CandyImage logo;

		private CandyButton bitizenButton;

		private CandyImage bitizenSeal;

		private CandyButton challengeButton;

		private CandyImage challengeBackground;

		private CandyImage challengePressedBackground;

		private int challengeCount;

		private CandyButton activeChallengeButton;

		private GameObject checkedBox;

		private GameObject uncheckedBox;

		private Transform topContainer;

		private CandyText challengeName;

		private CandyImage bottomBarImage;

		private CandyText versionWatermark;

		private Vector2 noSize;

		private bool isInChallenge;

		private List<GameObject> checkboxes;

		private int activeChallengeID;

		private Coroutine revealAnim;

		private UISeasonsDailyBar dailyRewardBarView;

		private ObjectiveType objectiveType;

		private NAV_BAR_BECOME_TYPE becomeType;

		private bool objectiveButtonIsShowing
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void OnShowStreakView(UISeasonsStreakBar view, bool visible)
		{
		}

		private void OnShowDailyRewardView(UISeasonsDailyBar view, bool visible)
		{
		}

		private void OnRectTransformDimensionsChange()
		{
		}

		private void LayoutDailyRewardBar()
		{
		}

		private float NavBarEdge(RectTransform element, bool right)
		{
			return 0f;
		}

		private void OnDisable()
		{
		}

		public void Init()
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		private void SetTheme(int themeIndex)
		{
		}

		private void UpdateChallengeNumbers()
		{
		}

		public void SetMainMenuButtonOnClickDelegateMethod(Action delegateMethod)
		{
		}

		public void SetupWatermark()
		{
		}

		public void SetupNavBar()
		{
		}

		private void UpdateBecomeButton()
		{
		}

		public void UpdateLogoAndBitizenButton()
		{
		}

		private void UpdateTheme()
		{
		}

		public void UpdateNavBar()
		{
		}

		public void UpdateNavBarTheme()
		{
		}

		public void UpdateChallengeButton()
		{
		}

		public void HideSnowWhenMenuOpensInTheWinter()
		{
		}

		public void UnHideSnowWhenMenuClosesInWinter()
		{
		}

		public void CompleteAchievement()
		{
		}

		private void SetHolidayOverlay()
		{
		}

		private void SetThemedOverlay()
		{
		}

		private void SetAchievementsPosTight()
		{
		}

		private void SetAchievementsPosNormal()
		{
		}

		private void TurnOnOverlay()
		{
		}

		private void TurnOffOverlay()
		{
		}

		private void SetLogoAndBitizenSeal()
		{
		}

		private void SwitchBecomeToBitizenSeal()
		{
		}

		public void BecomeBitizen()
		{
		}

		private void ChallengeButtonPressed()
		{
		}

		public void SetupObjectiveButtonForActiveChallenge(int overrideChallengeID = -1, bool forceUpdate = false)
		{
		}

		private void CheckBoxTearDownForActiveChallengeButton()
		{
		}

		private void CheckBoxSetupForActiveChallengeButton()
		{
		}

		private void BecomeButtonPressed()
		{
		}

		private void ObjectiveButtonPressed()
		{
		}

		private void UpdateObjectiveButton()
		{
		}

		public void RevealObjectiveButton()
		{
		}

		private IEnumerator RevealObjectiveButtonAnimation()
		{
			return null;
		}

		public void DismissObjectiveButtonForced()
		{
		}

		public void DismissObjectiveButton()
		{
		}

		private IEnumerator RemoveObjectiveButtonAnimation()
		{
			return null;
		}

		private IEnumerator ChallengeVaultButtonFade(float endValue)
		{
			return null;
		}
	}
}
