using System.Collections;

public class ScenarioMusicProducerArtistGift : Scenario
{
	private SimMusicProducerArtist _artist;

	private ArrayList _giftArray;

	private bool _requireRewardedVideo;

	public ScenarioMusicProducerArtistGift(SimMusicProducerArtist theArtist)
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
}
