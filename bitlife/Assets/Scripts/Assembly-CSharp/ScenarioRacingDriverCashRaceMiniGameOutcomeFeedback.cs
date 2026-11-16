public class ScenarioRacingDriverCashRaceMiniGameOutcomeFeedback : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCarDriver _driver;

	private readonly SimRaceCar _heroCar;

	private readonly float _cashAmount;

	private readonly RACING_CASH_OR_PINK_SLIP_RACE_OUTCOME _outcome;

	private readonly bool _allowGenericImpacts;

	public ScenarioRacingDriverCashRaceMiniGameOutcomeFeedback(SimRaceCarDriver driver, SimRaceCar heroCar, float cashAmount, RACING_CASH_OR_PINK_SLIP_RACE_OUTCOME outcome, bool aloowGenericImpacts = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void ImpactCarCondition(int impact)
	{
	}

	public override void DismissSelected()
	{
	}
}
