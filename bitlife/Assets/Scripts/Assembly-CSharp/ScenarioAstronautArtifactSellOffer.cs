public class ScenarioAstronautArtifactSellOffer : Scenario
{
	private readonly SimSpecialJobAstronaut _special;

	private readonly SimArtifact _artifact;

	private string _narrative;

	private string _customer;

	private readonly float _listPrice;

	private float _offer;

	private readonly DataStreetPerson _buyer;

	public ScenarioAstronautArtifactSellOffer(SimSpecialJobAstronaut special, SimArtifact artifact, float listPrice, DataStreetPerson buyer)
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
