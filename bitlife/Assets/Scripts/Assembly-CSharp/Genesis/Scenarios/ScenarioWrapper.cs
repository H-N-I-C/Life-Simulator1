using Genesis.Interfaces;

namespace Genesis.Scenarios
{
	public class ScenarioWrapper : IScenarioDataSource
	{
		private readonly Scenario _wrappedOther;

		public ScenarioWrapper(Scenario other)
		{
		}

		public bool AreChoicesRandomized()
		{
			return false;
		}

		public bool IsFeedbackScenario()
		{
			return false;
		}

		public string GenerateExteriorTitle()
		{
			return null;
		}

		public string GenerateTitle()
		{
			return null;
		}

		public string GenerateBody()
		{
			return null;
		}

		public ScenarioChoice[] GenerateScenarioChoices()
		{
			return null;
		}

		public IScenarioDataSource GetNextDataSourceForUserSelection(IScenarioUserSelection userSelection)
		{
			return null;
		}
	}
}
