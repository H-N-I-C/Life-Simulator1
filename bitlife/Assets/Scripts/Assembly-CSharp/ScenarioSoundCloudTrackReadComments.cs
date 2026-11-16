public class ScenarioSoundCloudTrackReadComments : Scenario
{
	private readonly SimSoundCloudArtist _artist;

	private readonly SimSoundCloudTrack _track;

	private BracketParser _parser;

	private string _narrative;

	public ScenarioSoundCloudTrackReadComments(SimSoundCloudTrack track)
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
