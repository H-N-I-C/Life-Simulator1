namespace Genesis.Interfaces
{
	public interface IScenarioDataSourceBarFeedback : IScenarioDataSource
	{
		ScenarioBarGraph[] BarGraphArray { get; }
	}
}
