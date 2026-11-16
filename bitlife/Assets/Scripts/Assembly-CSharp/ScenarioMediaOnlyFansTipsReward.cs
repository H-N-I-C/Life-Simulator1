using System.Collections.Generic;

public class ScenarioMediaOnlyFansTipsReward : Scenario
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public string selectedChoice;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataSocialMediaOnlyFansTipsReponse t)
		{
			return false;
		}
	}

	private readonly SimSocialMediaChannel _channel;

	private string _narrative;

	private readonly SimPerson _person;

	private readonly string _id;

	private List<DataSocialMediaOnlyFansTipsReponse> _listData;

	private BracketParser _parser;

	public ScenarioMediaOnlyFansTipsReward(SimSocialMediaChannel channel, SimPerson person, string id)
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
