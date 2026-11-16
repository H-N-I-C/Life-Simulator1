using Game;

public class FightClubAgeUpAnimalActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_AGEUP_ANIMAL_ENCOUNTER = 0,
		FC_AGEUP_ANIMAL_FEEDBACK = 1,
		FC_AGEUP_ANIMAL_WIN = 2,
		FC_AGEUP_ANIMAL_LOSE = 3,
		FC_AGEUP_ANIMAL_RUN = 4
	}

	private ScenarioId _currentScenarioId;

	private DataFightErAnimal AnimalData__BackingField;

	private string Circumstance__BackingField;

	private SimFightClub _fightClub;

	private SimHero _hero;

	private bool WillDie__BackingField;

	private int DamageImpact__BackingField;

	public string CurrentScenarioId => null;

	public DataFightErAnimal AnimalData
	{
		get
		{
			return AnimalData__BackingField;
		}
		private set
		{
			AnimalData__BackingField = value;
		}
	}

	public string Circumstance
	{
		get
		{
			return Circumstance__BackingField;
		}
		set
		{
			Circumstance__BackingField = value;
		}
	}

	private bool WillDie
	{
		get
		{
			return WillDie__BackingField;
		}
		set
		{
			WillDie__BackingField = value;
		}
	}

	public int DamageImpact
	{
		get
		{
			return DamageImpact__BackingField;
		}
		set
		{
			DamageImpact__BackingField = value;
		}
	}

	public FightClubAgeUpAnimalActivity(Life life)
		: base(null)
	{
	}

	private void OnChoiceMade(ChoiceSelection selection)
	{
	}

	private void OnFeedbackDismissed()
	{
	}

	private void PushWinFeedback()
	{
	}

	private void PushLoseFeedback()
	{
	}

	private void HandleDeath()
	{
	}

	private void CalculateDamage()
	{
	}

	public void ScenarioShown()
	{
	}
}
