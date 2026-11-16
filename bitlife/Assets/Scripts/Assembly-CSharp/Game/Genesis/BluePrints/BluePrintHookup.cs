using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintHookup : BluePrintNpcInteractionBase
	{
		private BluePrintData _data;

		private bool _configuredModeRequested;

		private bool _configuredPayloadIsValid;

		private bool _configuredRouteWasResolved;

		private string _heroReputationExpression;

		private string _npcFameExpression;

		private string _maleHeroFemaleNpcScenarioId;

		private string _femaleHeroMaleNpcScenarioId;

		private string _maleHeroMaleNpcScenarioId;

		private string _femaleHeroFemaleNpcScenarioId;

		private string _failureScenarioId;

		private Scenario _configuredScenario;

		public BluePrintHookup(ScenarioController controller)
			: base(null)
		{
		}

		public override void Setup(BluePrintData data)
		{
		}

		public static string ResolveSuccessScenarioId(int heroGender, int npcGender, string maleHeroFemaleNpc, string femaleHeroMaleNpc, string maleHeroMaleNpc, string femaleHeroFemaleNpc)
		{
			return null;
		}

		public override bool ConditionSelected()
		{
			return false;
		}

		public override Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		public static float CalculateAttractionScore(SimHero hero, SimPerson person, float heroReputation)
		{
			return 0f;
		}

		public static float CalculateResistance(SimPerson person, float npcFame)
		{
			return 0f;
		}

		public static bool MeetsAttractionThreshold(SimHero hero, SimPerson person, float heroReputation, float npcFame)
		{
			return false;
		}

		private Scenario EnsureConfiguredRoute()
		{
			return null;
		}

		private bool TryResolveFiniteFloat(string expression, out float value)
		{
			value = default;
			return false;
		}

		private static bool IsFinite(float value)
		{
			return false;
		}

		private static string NormalizeScenarioId(string value)
		{
			return null;
		}

		private static bool IsFullScenarioId(string value)
		{
			return false;
		}
	}
}
