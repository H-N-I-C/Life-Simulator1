public class ScenarioCasinoOutcome : Scenario
{
	protected SimCasinoSession _session;

	private string _gameTypeString;

	private bool _ownsGoldenNeko;

	private float _additionalAmount;

	public ScenarioCasinoOutcome(SimCasinoSession theSession)
	{
	}

	public override void DismissSelected()
	{
	}
}
