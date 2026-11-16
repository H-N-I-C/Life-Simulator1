using System.Collections;

public class ScenarioMusicAlbumConfigure : Scenario
{
	public SimSpecialJobMusician _specialJob;

	public SimMusicAlbum _album;

	public int theAlbumType;

	public bool _push;

	public bool _requireRewardedVideo;

	public ArrayList _albumCoverDataArray;

	public ArrayList _albumCoverStringArray;

	public ScenarioMusicAlbumConfigure(SimSpecialJobMusician theSpecialJob, int theAlbumType, bool isPush)
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
