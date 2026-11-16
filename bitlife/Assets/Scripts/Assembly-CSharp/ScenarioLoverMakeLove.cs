public class ScenarioLoverMakeLove : Scenario
{
	protected SimLover _lover;

	protected bool _forceAgreement;

	protected bool _isConjugalVisit;

	protected string _narrative;

	public float HeroEnjoyment;

	public float LoverEnjoyment;

	private string OverrideTitle__BackingField;

	private string OverrideNarrative__BackingField;

	public string OverrideTitle
	{
		get
		{
			return OverrideTitle__BackingField;
		}
		set
		{
			OverrideTitle__BackingField = value;
		}
	}

	public string OverrideNarrative
	{
		get
		{
			return OverrideNarrative__BackingField;
		}
		set
		{
			OverrideNarrative__BackingField = value;
		}
	}

	public ScenarioLoverMakeLove(SimLover theLover, bool theForceAgreement)
	{
	}

	public ScenarioLoverMakeLove(SimLover theLover)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	private void CalculateEnjoyment()
	{
	}
}
