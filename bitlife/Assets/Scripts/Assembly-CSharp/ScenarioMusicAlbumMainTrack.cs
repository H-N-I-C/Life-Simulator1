using System.Collections;

public class ScenarioMusicAlbumMainTrack : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	protected SimMusicAlbum _album;

	private bool _push;

	public ScenarioMusicAlbumMainTrack(SimSpecialJobMusician theSpecialJob, SimMusicAlbum theAlbum, bool isPush)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
