public class SimBangerzAwardsBestProducer : SimBangerzAwardsNomination
{
	private Life _life;

	private SimHero _hero;

	private SimSpecialJobMusicProducer _specialJob;

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
