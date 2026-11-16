using System.Collections;

public class ScenarioVampireCouncilMemberGift : Scenario
{
	private readonly SimVampireCouncilMember _member;

	private ArrayList _giftArray;

	private bool _requireRewardedVideo;

	public ScenarioVampireCouncilMemberGift(SimVampireCouncilMember theMember)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
