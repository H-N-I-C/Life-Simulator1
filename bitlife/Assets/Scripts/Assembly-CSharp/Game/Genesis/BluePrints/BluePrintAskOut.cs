using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintAskOut : BluePrintNpcInteractionBase
	{
		private bool _configuredModeRequested;

		private bool _configuredPayloadIsValid;

		private bool _configuredRouteWasResolved;

		private float _threshold;

		private string _successScenarioId;

		private string _failureScenarioId;

		private Scenario _configuredScenario;

		public BluePrintAskOut(ScenarioController controller)
			: base(null)
		{
		}

		public override void Setup(BluePrintData data)
		{
		}

		public override bool ConditionSelected()
		{
			return false;
		}

		public override Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		public static float CalculateDateScore(SimHero hero, SimPerson person)
		{
			return 0f;
		}

		public static bool MeetsDateThreshold(SimHero hero, SimPerson person, float threshold)
		{
			return false;
		}

		private Scenario EnsureConfiguredRoute()
		{
			return null;
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
