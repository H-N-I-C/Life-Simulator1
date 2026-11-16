namespace Genesis.Interfaces
{
	public interface IScenarioDataSource
	{
		bool AreChoicesRandomized();

		bool IsFeedbackScenario();

		string GenerateExteriorTitle();

		string GenerateTitle();

		string GenerateBody();

		ScenarioChoice[] GenerateScenarioChoices();

		IScenarioDataSource GetNextDataSourceForUserSelection(IScenarioUserSelection userSelection);
	}
}
