using System;

public class ScenarioSportsTeamJoined : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__11_0;

		internal bool _003CChoiceSelectedAtIndex_003Eb__11_0(Scenario x)
		{
			return false;
		}
	}

	protected SimSportsTeam _team;

	protected SimSpecialJobAthlete _specialJob;

	protected DataSportsPosition _position;

	private float _contractValue;

	private int _contractYears;

	private bool _allowNegotiation;

	private bool _newContractAtCurrentTeam;

	protected DataCountry _foreignTeamCountry;

	private string _narrative;

	public ScenarioSportsTeamJoined(SimSportsTeam theTeam, DataSportsPosition thePosition, float theContractValue, int theContractYears)
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
