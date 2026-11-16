public class ScenarioMusicAlbumAward : Scenario
{
	public SimMusicAlbum _album;

	public string _narrative;

	public ScenarioMusicAlbumAward(SimMusicAlbum theAlbum)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
