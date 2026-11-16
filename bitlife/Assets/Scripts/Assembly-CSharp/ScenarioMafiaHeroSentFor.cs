public class ScenarioMafiaHeroSentFor : Scenario
{
	protected bool _feedbackNegative;

	protected bool _feedbackPositive;

	protected bool _feedbackEarnings;

	protected bool _beating;

	protected string _beatingReason;

	protected bool _canGetAway;

	protected SimSpecialJobMafia _specialJob;

	public ScenarioMafiaHeroSentFor()
	{
	}

	public ScenarioMafiaHeroSentFor(bool feedbackEarnings, bool feedbackNegative, bool feedbackPositive)
	{
	}

	public ScenarioMafiaHeroSentFor(string theReason, bool theCanGetAway)
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
