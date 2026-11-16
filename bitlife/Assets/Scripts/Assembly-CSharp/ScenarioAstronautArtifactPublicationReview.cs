public class ScenarioAstronautArtifactPublicationReview : Scenario
{
	private readonly SimArtifact _artifact;

	private string _narrative;

	private readonly string _titleName;

	private readonly SimSpecialJobAstronaut _special;

	public ScenarioAstronautArtifactPublicationReview(SimSpecialJobAstronaut special, SimArtifact artifact, string titleName = "")
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
