using Genesis.Interfaces;

namespace Genesis.DataSources
{
	public class ScriptableScenarioDataSource : AbstractScriptableDataSource
	{
		private string _ExteriorTitleFormat;

		private string _TitleFormat;

		private bool m_FeedbackStyleScenario;

		private string _BodyFormat;

		private ScriptableScenarioDataSource[] _NextNodes;

		public override bool AreChoicesRandomized()
		{
			return false;
		}

		public override bool IsFeedbackScenario()
		{
			return false;
		}

		public override string GenerateExteriorTitle()
		{
			return null;
		}

		public override string GenerateTitle()
		{
			return null;
		}

		public override string GenerateBody()
		{
			return null;
		}

		public override ScenarioChoice[] GenerateScenarioChoices()
		{
			return null;
		}

		public override IScenarioDataSource GetNextDataSourceForUserSelection(IScenarioUserSelection userSelection)
		{
			return null;
		}
	}
}
