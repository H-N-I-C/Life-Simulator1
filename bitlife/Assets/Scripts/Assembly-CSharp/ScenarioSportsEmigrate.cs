using System.Collections;

public class ScenarioSportsEmigrate : Scenario
{
	protected DataCountry _country;

	protected SimSportsTeam _team;

	protected DataSportsPosition _position;

	protected float _contractValue;

	protected int _contractYears;

	private string _narrative;

	private bool _currentLoverGoing;

	protected ArrayList _emigratingChildArray;

	public ScenarioSportsEmigrate(DataCountry theCountry, SimSportsTeam theTeam, DataSportsPosition thePosition, float theContractValue, int theContractYears)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
