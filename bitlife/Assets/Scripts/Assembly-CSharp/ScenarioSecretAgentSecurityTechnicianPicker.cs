using Game;

public class ScenarioSecretAgentSecurityTechnicianPicker : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSpyAgencySecurityFeature _feature;

	private readonly DataSecretAgentAgencySecurityFeature _featureData;

	private readonly string _techCodeName;

	private float _credibility;

	private double _cost;

	public ScenarioSecretAgentSecurityTechnicianPicker(SimSpyAgencySecurityFeature feature, string previousTechCodeName = "")
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

	private bool _003CChoiceSelectedAtIndex_003Eb__9_0(SimSpyAgencySecurityFeature x)
	{
		return false;
	}
}
