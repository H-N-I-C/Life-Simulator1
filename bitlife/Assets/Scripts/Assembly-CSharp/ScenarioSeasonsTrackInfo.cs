using Game.Seasons.Models;

public class ScenarioSeasonsTrackInfo : Scenario
{
	private readonly TrackItemModel _item;

	private readonly bool _isPremium;

	public override string EmojiString()
	{
		return null;
	}

	public ScenarioSeasonsTrackInfo(TrackItemModel item, bool isPremium)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected void Refresh()
	{
	}
}
