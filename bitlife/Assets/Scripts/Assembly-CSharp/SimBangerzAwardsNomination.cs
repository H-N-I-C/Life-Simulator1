public abstract class SimBangerzAwardsNomination
{
	public abstract bool IsNominated(Life life, SimSpecialJobMusicProducer specialJob);

	public abstract bool TryWin();

	public abstract void Accept();
}
