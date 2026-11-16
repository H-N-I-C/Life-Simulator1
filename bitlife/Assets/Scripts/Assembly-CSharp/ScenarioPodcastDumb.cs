using System;
using Genesis.CSV;
using Genesis.CSV.CSVTranslators.Primitives;
using Genesis.Interfaces;
using Genesis.Scenarios;

public class ScenarioPodcastDumb : GenericScenario
{
	private class ViralFeedbackDataSource : IScenarioDataSource
	{
		private readonly SimPodcastActivity _podcast;

		public ViralFeedbackDataSource(SimPodcastActivity podcastActivity)
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

	private class PodcastDataSource : IScenarioDataSource
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<ScenarioChoice, bool> _003C_003E9__14_0;

			internal bool _003CGenerateScenarioChoices_003Eb__14_0(ScenarioChoice choice)
			{
				return false;
			}
		}

		private const string PODCAST_DUMB_CSV_RESOURCE_PATH = "data/a16_podcast-dumb-enc";

		private readonly SimPodcastActivity _activity;

		private readonly CSVDocument _podcastDumbCSV;

		private readonly IntegerTranslator _integerTranslator;

		private readonly BooleanTranslator _booleanTranslator;

		private readonly StringTranslator _stringTranslator;

		private readonly BracketParser _bracketParser;

		private readonly CSVLine _randomDumbChosen;

		public PodcastDataSource(SimPodcastActivity activity)
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

		private void RunChoiceSelection(int choice)
		{
		}

		private bool _003C_002Ector_003Eb__8_0(CSVLine line)
		{
			return false;
		}
	}

	public override string EmojiString()
	{
		return null;
	}

	public ScenarioPodcastDumb(SimPodcastActivity activity)
		: base(null)
	{
	}
}
