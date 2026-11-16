public interface IPushScenarioConfigEntry
{
	string ScenarioCategory { get; }

	int Priority { get; }

	int MinDraws { get; }

	int MaxDraws { get; }

	float DrawProbability { get; }

	int AgeMin { get; }

	int AgeMax { get; }
}
