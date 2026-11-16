namespace Game.Seasons.Controllers
{
	public enum LoadNarrativeState
	{
		None = 0,
		NetworkFailure = 1,
		NetworkSuccess = 2,
		AlreadyCompleted = 3,
		WatchRvToContinue = 4,
		NeedToWatchRv = 5
	}
}
