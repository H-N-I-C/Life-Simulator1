using System;

[Serializable]
public class ScenarioHustleEarnings : Scenario
{
	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected int _hustleType;

	protected string _narrative;

	protected float _earnings;

	protected bool _sessionHaul;

	protected bool _recoveredInvalidBuskingHustle;

	protected string _invalidBuskingReason;

	public ScenarioHustleEarnings(bool isSessionHaul, int hustleType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public float GetAnnualHaulCash()
	{
		return 0f;
	}

	public override void DismissSelected()
	{
	}
}
