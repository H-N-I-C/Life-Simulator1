using System.Collections;

public class ScenarioMafiaCrimeDelegate : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected string _presentSnippet;

	protected string _pastSnippet;

	protected DataMafiaPushCrime _pushCrime;

	protected DataMafiaPushTarget _pushTarget;

	protected int _pushTargetGender;

	protected bool _isShakedown;

	public ScenarioMafiaCrimeDelegate(SimSpecialJobMafia theSpecialJob, string thePresentSnippet, string thePastSnippet, DataMafiaPushCrime thePushCrime)
	{
	}

	public ScenarioMafiaCrimeDelegate(SimSpecialJobMafia theSpecialJob, string thePresentSnippet, string thePastSnippet, DataMafiaPushTarget thePushTarget, int thePushTargetGender, bool theIsShakedown)
	{
	}

	public override void RefreshScenario()
	{
	}

	public new void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
