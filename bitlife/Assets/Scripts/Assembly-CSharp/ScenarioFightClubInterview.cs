using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioFightClubInterview : ScenarioLazyCsvBase
{
	public const string REWARDED_VIDEO_CHOICE = "Tell a convincing story";

	private readonly FightClubInterviewActivity _activity;

	private readonly FightClubPostLeagueData _data;

	private Dictionary<string, DataFightIngLossReason> _lossReasons;

	private Dictionary<string, DataFightIngWinReason> _winReasons;

	private bool _hasRewardedOption;

	public ScenarioFightClubInterview(FightClubInterviewActivity activity, ChoiceCallback choiceCallback = null, DismissCallback dismissCallback = null)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void ShowDropdownLoseReasons()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void ShowDropdownWinReasons()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
