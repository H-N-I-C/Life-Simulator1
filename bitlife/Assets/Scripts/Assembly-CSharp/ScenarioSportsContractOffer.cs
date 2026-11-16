public class ScenarioSportsContractOffer : Scenario
{
	protected SimSportsTeam _team;

	protected DataSportsPosition _position;

	protected SimSportsContract _contract;

	protected float _contractValue;

	protected int _contractYears;

	protected bool _allowNegotiation;

	protected bool _newContractAtCurrentTeam;

	protected DataCountry _foreignTeamCountry;

	public ScenarioSportsContractOffer(SimSportsTeam theTeam, DataSportsPosition thePosition, SimSportsContract theContract, bool shouldAllowNegotiation)
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
