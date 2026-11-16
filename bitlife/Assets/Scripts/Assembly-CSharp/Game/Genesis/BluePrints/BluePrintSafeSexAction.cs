using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public sealed class BluePrintSafeSexAction : IBluePrint, IAbortBlueprintChainOnFailure
	{
		public const string HeroEnjoymentMemoryKey = "mdHookupHeroEnjoyment";

		public const string NpcEnjoymentMemoryKey = "mdHookupNpcEnjoyment";

		public const string PendingNpcIdMemoryKey = "mdHookupPendingNpcId";

		public const string OutcomeReadyMemoryKey = "mdHookupOutcomeReady";

		private ScenarioController _controller;

		private bool _payloadIsValid;

		private bool _executed;

		private bool _succeeded;

		private int _relationshipImpact;

		private int _stdChance;

		private int _pregnancyChance;

		private int _enjoymentMin;

		private int _enjoymentMax;

		private string _nextScenarioId;

		private Scenario _nextScenario;

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

		public void Setup(BluePrintData data)
		{
		}

		public bool ConditionSelected()
		{
			return false;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private void EnsureExecuted()
		{
		}

		public static int DrawInclusiveScore(int min, int max)
		{
			return 0;
		}

		private static SimDisease TryTransmitActualNpcStd(SimHero hero, SimPerson npc, int chance)
		{
			return null;
		}

		private static SimPregnancy TryCreatePregnancy(SimHero hero, SimPerson npc, int chance)
		{
			return null;
		}

		private static bool TryParsePercent(string raw, out int value)
		{
			value = default;
			return false;
		}

		private static bool TryParseScore(string raw, out int value)
		{
			value = default;
			return false;
		}

		private static bool IsFullScenarioId(string value)
		{
			return false;
		}

		private static bool ReadBool(object raw)
		{
			return false;
		}

		private static void SetTransient(ScenarioController controller, string key, object value)
		{
		}

		internal static void ClearTransient(ScenarioController controller, string[] keys)
		{
		}

		private static void ClearLegacyPersistentScratch(ScenarioController controller)
		{
		}
	}
}
