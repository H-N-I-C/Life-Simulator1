using System.Collections;

public class ScenarioDiseaseContract : Scenario
{
	protected SimDisease _disease;

	protected DataDiseaseSymptomScenario _symptomScenario;

	protected ArrayList _symptomArray;

	protected string _narrative;

	public ScenarioDiseaseContract(SimDisease theDisease)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	private new bool PreventRandomPushAfterScenario()
	{
		return false;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
