public interface IScenarioComponentEvent
{
	ScenarioComponentEvent ScenarioEventData { get; }

	void SetScenarioEventData(ScenarioComponentEvent data);
}
