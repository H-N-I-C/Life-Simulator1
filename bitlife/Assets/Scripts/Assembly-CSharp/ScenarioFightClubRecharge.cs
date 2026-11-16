public class ScenarioFightClubRecharge : ScenarioLazyCsvBase
{
	private readonly FightClubRechargeActivity _recharge;

	public const string REWARDED_VIDEO_CHOICE = "Get VIP treatment instead";

	public ScenarioFightClubRecharge(FightClubRechargeActivity activity)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	private void PopulateRewardedVideoChoices()
	{
	}

	private void FormatMethodDropDown()
	{
	}
}
