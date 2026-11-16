using Game.Seasons.Models;

public class ScenarioSeasonsUnlockBitPass : ScenarioSeasonsTrackInfo
{
	public override string EmojiString()
	{
		return null;
	}

	public ScenarioSeasonsUnlockBitPass(TrackItemModel item, bool isPremium)
		: base(null, isPremium: false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
