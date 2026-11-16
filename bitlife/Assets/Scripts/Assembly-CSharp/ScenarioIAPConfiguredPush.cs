using System.Collections.Generic;

public class ScenarioIAPConfiguredPush : Scenario
{
	private const string DEFAULT_EMOJI = "memo";

	private readonly IPushScenarioContent _iapContent;

	private BracketParser _bracketParser;

	public ScenarioIAPConfiguredPush(IPushScenarioContent content)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void AcceptIAP()
	{
	}

	private void DeclineIAP()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private string GetTelemetrySubcategory(bool interest)
	{
		return null;
	}

	private Dictionary<string, string> CreateBodyBracketDictionary()
	{
		return null;
	}

	private Dictionary<string, string> CreateNarrativeBracketDictionary()
	{
		return null;
	}

	private string GetCarString()
	{
		return null;
	}

	private string GetSpaceAgencyString()
	{
		return null;
	}
}
