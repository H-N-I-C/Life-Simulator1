using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioTrackProductionDetails : Scenario
{
	private string _narrative;

	private MusicProducerTrackProductionActivity _activity;

	private BracketParser _parser;

	private Dictionary<string, DataMusicTrackSubject> _subjects;

	private Dictionary<string, DataMusicTrackBeat> _beats;

	private Dictionary<string, DataMusicTrackLyricalHook> _hooks;

	public ScenarioTrackProductionDetails(MusicProducerTrackProductionActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void BuildDropdownArrays()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void PushRejectionFeedback()
	{
	}

	private void PushApprovedScenarios()
	{
	}

	private Scenario GetProductionScenario(int scenarioIndex)
	{
		return null;
	}

	private void UpdateNarrative()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private string _003CRefreshScenario_003Eb__7_0(string q)
	{
		return null;
	}
}
