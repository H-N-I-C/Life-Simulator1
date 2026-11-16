public class ScenarioMusicAlbumNewBand : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	protected SimMusicAlbum _album;

	protected DataMusicAlbumCover _albumCoverData;

	public ScenarioMusicAlbumNewBand(SimSpecialJobMusician theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
