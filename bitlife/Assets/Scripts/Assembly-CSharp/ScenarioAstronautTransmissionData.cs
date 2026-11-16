public class ScenarioAstronautTransmissionData : Scenario
{
	private readonly SimTransmission _transmission;

	private string _narrative;

	private readonly SimSpecialJobAstronaut _special;

	private readonly bool _showPublish;

	public ScenarioAstronautTransmissionData(SimSpecialJobAstronaut special, SimTransmission transmission, bool showPublish = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
