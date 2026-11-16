public class ScenarioMusicAlbumStats : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	protected SimMusicAlbum _album;

	private bool _justReleased;

	private string _narrative;

	public ScenarioMusicAlbumStats(SimMusicAlbum theAlbum, bool wasJustReleased)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
