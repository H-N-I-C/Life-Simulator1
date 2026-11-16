public class FightClubCoachActionsActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_COACH_PROFILE = 0,
		FC_COACH_PROFILE_NEW_COACH = 1,
		FC_COACH_FIRE = 2,
		FC_COACH_NOT_AVAILABLE = 3,
		FC_COACH_NO_MONEY = 4,
		FC_COACH_HIRE_SUCCESS = 5,
		FC_COACH_DEATH = 6
	}

	public enum EntryPoint
	{
		PROFILE = 0,
		FIRE = 1,
		COACH_DEATH = 2
	}

	private ScenarioId _currentScenarioId;

	private SimFightClubCoach TargetCoach__BackingField;

	private string CoachQualityColor__BackingField;

	private SimFightClubCoach PreviousCoach__BackingField;

	private SimHero _hero;

	private SimFightClub _fightClub;

	private SimFightClubCoach _currentCoach;

	private SimFightClubCoach _coachCandidate;

	public string CurrentScenarioId => null;

	public SimFightClubCoach TargetCoach
	{
		get
		{
			return TargetCoach__BackingField;
		}
		private set
		{
			TargetCoach__BackingField = value;
		}
	}

	public string CoachQualityColor
	{
		get
		{
			return CoachQualityColor__BackingField;
		}
		set
		{
			CoachQualityColor__BackingField = value;
		}
	}

	public double CoachCost => 0.0;

	public double CredBalance => 0.0;

	public SimFightClubCoach PreviousCoach
	{
		get
		{
			return PreviousCoach__BackingField;
		}
		private set
		{
			PreviousCoach__BackingField = value;
		}
	}

	public FightClubCoachActionsActivity(Life life, EntryPoint entryPoint)
		: base(null)
	{
	}

	private void OnChoiceMade(ChoiceSelection selection)
	{
	}

	private void CreateCoachCandidate()
	{
	}

	private void OnCoachChoiceMade(ChoiceSelection selection)
	{
	}

	private void HireCoach()
	{
	}
}
