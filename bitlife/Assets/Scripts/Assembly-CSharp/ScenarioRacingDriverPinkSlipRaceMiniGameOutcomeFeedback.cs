public class ScenarioRacingDriverPinkSlipRaceMiniGameOutcomeFeedback : Scenario
{
	private string _narrative;

	private readonly SimRacing _racing;

	private readonly SimRaceCarDriver _driver;

	private readonly SimRaceCar _heroCar;

	private readonly RACING_CASH_OR_PINK_SLIP_RACE_OUTCOME _outcome;

	private readonly bool _allowGenericImpacts;

	public ScenarioRacingDriverPinkSlipRaceMiniGameOutcomeFeedback(SimRaceCarDriver driver, SimRaceCar heroCar, RACING_CASH_OR_PINK_SLIP_RACE_OUTCOME outcome, bool aloowGenericImpacts = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void ImpactCarCondition(int impact, SimRaceCar car)
	{
	}

	public override void DismissSelected()
	{
	}
}
