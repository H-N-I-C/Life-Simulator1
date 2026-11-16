using System.Collections.Generic;

public class ScenarioModelGoSeesPerformanceReviewFeedback : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private BracketParser _parser;

	private readonly List<float> _clientRelationshipDiffs;

	private float _impression;

	public ScenarioModelGoSeesPerformanceReviewFeedback(List<float> clientRelationshipDiffs)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
