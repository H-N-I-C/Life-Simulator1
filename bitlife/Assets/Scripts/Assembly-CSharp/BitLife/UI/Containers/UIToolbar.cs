using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIToolbar : MonoBehaviour
	{
		private const string INFANT = "Infant";

		private const string OCCUPATION = "Occupation";

		private const string SCHOOL = "School";

		private const string JOB = "Job";

		private const string PRISON = "Prison";

		private const string SEASON_DEAD = "Time is Up!";

		private const string JUVIE = "Juvie";

		private const string DEATH = "Death";

		private const string SMALLBUTTON2 = "Assets";

		private const string SMALLBUTTON3 = "Relationships";

		private const string SMALLBUTTON4 = "Activities";

		private const string AGEBUTTON = "Age";

		private const float HEIGHT = 252f;

		private const float WIDTH = 1242f;

		private const int SMALL_BUTTON_COUNT = 4;

		public readonly int SMALL_BUTTON_STATE_COUNT;

		public readonly int BIG_BUTTON_STATE_COUNT;

		public readonly int AGE_DOWN_STATE_COUNT;

		public readonly int TOOLBAR_STATE_COUNT;

		private const float ALPHA_THRESHOLD = 0.001f;

		private readonly Color grayColor;

		private AccessibleUIGroupRoot uapGroup;

		private CandyImage background;

		private CandyImage buttonsBackgroud;

		public CandyButton smallButton1;

		public CandyButton smallButton2;

		public CandyButton smallButton3;

		public CandyButton smallButton4;

		public CandyButton ageDownButton;

		public CandyButton bigButton_NewLife;

		public CandyButton bigButton_AgeUp;

		public AnimationImageSequence NewLifeSequence;

		public AnimationImageSequence NewLifeSequencePressed;

		public AccessibleCandyButton smallButton1UAP;

		private GameObject partiallyDisabledOverlay;

		private GameObject completelyDisabledOverlay;

		private ConstraintScaleBgUI scale;

		private int currentState_ButtonOne;

		private UIStatesBigB currentState_BigButton;

		private UIStatesAgeDown currentState_AgeDown;

		private UIStatesToolbar currentState_Toolbar;

		private bool ageDownIsOff;

		private int fontType;

		private int themeIndex;

		public AccessibleUIGroupRoot UapGroup => null;

		public void Init()
		{
		}

		public void SetAgeUpPartialClickablility()
		{
		}

		public int GetT1ButtonState()
		{
			return 0;
		}

		public UIStatesBigB GetCurrentState_BigB()
		{
			return UIStatesBigB.STATE_BIGB_NEWLIFE;
		}

		public UIStatesAgeDown GetCurrentState_AgeDown()
		{
			return UIStatesAgeDown.STATE_AGE_DOWN_OFF;
		}

		public UIStatesToolbar GetCurrentState_Toolbar()
		{
			return UIStatesToolbar.STATE_TOOLBAR_FRESH;
		}

		public void ChangeState_ButtonOne(int state)
		{
		}

		private void UpdateT1()
		{
		}

		public void ChangeState_BigButton(UIStatesBigB state)
		{
		}

		public void ChangeState_AgeDown(UIStatesAgeDown state)
		{
		}

		public void SetToolbarState(int state)
		{
		}

		private void TurnOn_PartiallyDisabledOverlay()
		{
		}

		private void TurnOff_PartiallyDisabledOverlay()
		{
		}

		private void TurnOn_CompletelyDisabledOverlay()
		{
		}

		private void TurnOff_CompletelyDisabledOverlay()
		{
		}

		private void ToolbarFresh()
		{
		}

		private void ToolbarAlive()
		{
		}

		private void ToolbarJailed()
		{
		}

		private void ToolbarSeasonTimeout()
		{
		}

		private void ToolbarDead()
		{
		}

		public void HandleAgeDownState(int forcedState = -1)
		{
		}

		public bool IsTButtonActiveAndAvailable(int TIndex)
		{
			return false;
		}

		private void NewLife()
		{
		}

		private void AgeUp()
		{
		}

		private void AgeDownOff()
		{
		}

		private void AgeDownDisabled()
		{
		}

		private void AgeDownTimeMachine()
		{
		}

		private void DisableSmallButtons2to4()
		{
		}

		private void DisableAllSmallButtons()
		{
		}

		private void EnableAllSmallButttons()
		{
		}

		public void UpdateToolBar(int T1ButtonState)
		{
		}

		public void UpdateToolBarTheme()
		{
		}

		private void SetNewLifeTheme()
		{
		}

		private void UpdateTs()
		{
		}

		public void SetupBackground()
		{
		}

		public void SetupButtonBackground()
		{
		}

		public void TranslateBar()
		{
		}
	}
}
