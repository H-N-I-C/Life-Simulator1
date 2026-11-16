using System;
using Genesis.CSV;
using Genesis.CSV.CSVTranslators.Primitives;
using Genesis.Interfaces;
using Genesis.Scenarios;

namespace Game.BackToSchool.SleepOver
{
	public class ScenarioBackToSchoolSleepoverDumb : GenericScenario
	{
		private class SleepoverDataSource : IScenarioDataSource
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

			private const string kDumbCsvResourcePath = "data/a16_backtoschool-sleepover-invitation-dumb-enc";

			private readonly CSVDocument _sleepoverDumbCsv;

			private readonly SleepoverActivity _activity;

			private readonly BracketParser _bracketParser;

			private readonly IntegerTranslator _integerTranslator;

			private readonly BooleanTranslator _booleanTranslator;

			private readonly StringTranslator _stringTranslator;

			private readonly CSVLine _randomDumbChosen;

			public SleepoverDataSource(SleepoverActivity activity)
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

		private class SleepoverFeedbackDataSource : IScenarioDataSourceBarFeedback, IScenarioDataSource
		{
			private SleepoverActivity _activity;

			public ScenarioBarGraph[] BarGraphArray => null;

			public SleepoverFeedbackDataSource(SleepoverActivity activity)
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

		public override string EmojiString()
		{
			return null;
		}

		public ScenarioBackToSchoolSleepoverDumb(SleepoverActivity activity)
			: base(null)
		{
		}
	}
}
