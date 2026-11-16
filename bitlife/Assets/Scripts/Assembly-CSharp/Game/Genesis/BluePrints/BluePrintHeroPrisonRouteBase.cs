using System.Collections;

namespace Game.Genesis.BluePrints
{
	public abstract class BluePrintHeroPrisonRouteBase : IBluePrint
	{
		protected string CrimeName;

		protected int SentenceYears;

		protected string PrisonName;

		private BluePrintType BluePrintType__BackingField;

		public BluePrintType BluePrintType
		{
			get
			{
				return BluePrintType__BackingField;
			}
			set
			{
				BluePrintType__BackingField = value;
			}
		}

		public virtual void Setup(BluePrintData data)
		{
		}

		public bool ConditionSelected()
		{
			return false;
		}

		public abstract Scenario GetNextScenario(string followupScenario);

		protected ArrayList BuildCrimeArray()
		{
			return null;
		}

		private static int ResolveCrimeType(string crimeName)
		{
			return 0;
		}

		private static bool TryParseSentenceYears(string rawValue, out int sentenceYears)
		{
			sentenceYears = default;
			return false;
		}
	}
}
