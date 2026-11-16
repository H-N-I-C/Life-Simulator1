public class ScenarioCasinoCelebrityMeetAndGreetResult : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoCelebrity _celebrity;

	private readonly SimCasinoGameRoom _gameRoom;

	private readonly float _fee;

	private readonly int _ticketsSold;

	private readonly (string, bool) _reason;

	public ScenarioCasinoCelebrityMeetAndGreetResult(SimCasinoCelebrity celebrity, SimCasinoGameRoom gameRoom, float fee, int ticketsSold, (string, bool) reason)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
