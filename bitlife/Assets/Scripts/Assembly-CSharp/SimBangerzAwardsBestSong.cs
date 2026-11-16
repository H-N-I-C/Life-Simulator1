public class SimBangerzAwardsBestSong : SimBangerzAwardsNomination
{
	private SimMusicProducerTrack NominatedTrack__BackingField;

	private Life _life;

	private SimHero _hero;

	private SimSpecialJobMusicProducer _specialJob;

	public SimMusicProducerTrack NominatedTrack
	{
		get
		{
			return NominatedTrack__BackingField;
		}
		private set
		{
			NominatedTrack__BackingField = value;
		}
	}

	public override bool IsNominated(Life life, SimSpecialJobMusicProducer specialJob)
	{
		return false;
	}

	public override bool TryWin()
	{
		return false;
	}

	public override void Accept()
	{
	}
}
