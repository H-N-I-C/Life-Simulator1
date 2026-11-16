using Game;

public class ScenarioCasinoAmenityInteractionGambler : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoAmenity _amenity;

	private SimCasinoGambler _gambler;

	private bool _positiveInteraction;

	private DataCasinoGamblerInteractionScenario _result;

	public ScenarioCasinoAmenityInteractionGambler(SimCasinoAmenity amenity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
