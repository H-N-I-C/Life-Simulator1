using Game;

public class FightClubAgeUpFamilyActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_AGEUP_CONCERN = 0,
		FC_AGEUP_CONCERN_REASSURE = 1,
		FC_AGEUP_CONCERN_YELL = 2,
		FC_AGEUP_ADMIRATION = 3,
		FC_AGEUP_ADMIRATION_THANK = 4,
		FC_AGEUP_ADMIRATION_COMPLIMENT = 5
	}

	public enum EntryPoint
	{
		Concern = 0,
		Admiration = 1
	}

	private ScenarioId _currentScenarioId;

	private SimHero _hero;

	private DataFightErConcernScenario ConcernScenario__BackingField;

	private DataFightErAdmirationScenario AdmirationScenario__BackingField;

	private string ComplimentString__BackingField;

	private FightClubHelpers.FightClubFamiliar _familiar;

	public string CurrentScenarioId => null;

	public SimPerson Familiar => null;

	public DataFightErConcernScenario ConcernScenario
	{
		get
		{
			return ConcernScenario__BackingField;
		}
		private set
		{
			ConcernScenario__BackingField = value;
		}
	}

	public DataFightErAdmirationScenario AdmirationScenario
	{
		get
		{
			return AdmirationScenario__BackingField;
		}
		private set
		{
			AdmirationScenario__BackingField = value;
		}
	}

	public string ComplimentString
	{
		get
		{
			return ComplimentString__BackingField;
		}
		private set
		{
			ComplimentString__BackingField = value;
		}
	}

	public FightClubAgeUpFamilyActivity(Life life, EntryPoint entry)
		: base(null)
	{
	}

	private void BuildAdmirationEntry()
	{
	}

	private void BuildConcernEntry()
	{
	}

	private void OnAdmirationChoiceMade(ChoiceSelection selection)
	{
	}

	private void OnConcernChoiceMade(ChoiceSelection selection)
	{
	}

	private void SelectFamiliar()
	{
	}

	public void ScenarioShown()
	{
	}
}
