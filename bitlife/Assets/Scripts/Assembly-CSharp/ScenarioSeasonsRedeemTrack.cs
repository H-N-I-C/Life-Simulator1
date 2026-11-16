using Backend;

public class ScenarioSeasonsRedeemTrack : Scenario
{
	public enum Source
	{
		BitPass = 0,
		Shop = 1
	}

	private MYSTUFF_CATEGORY _category;

	private readonly StuffItem _item;

	private readonly Source _scenarioSource;

	private bool _launchGenesisContent;

	private const string SpecialLifeHeader = "<b><color=#4D9946>PURCHASE COMPLETE!\nLIMITED-TIME ACCESS GRANTED!\nYOUR TIME STARTS NOW!</color></b>";

	private const string SpecialLifeInfo = "When the clock runs out, your life is locked! Complete tasks to play longer. View this life anytime in \"My Stuff.\"";

	public ScenarioSeasonsRedeemTrack(StuffItem item, Source scenarioSource = Source.BitPass)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void DismissSelected()
	{
	}

	private void _003CChoiceSelectedAtIndex_003Eb__9_0(bool success, string errorMessage)
	{
	}
}
