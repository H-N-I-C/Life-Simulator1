using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintHeroDeath : BluePrintNpcInteractionBase
	{
		private int _deathType;

		private string _deathReason;

		private bool _deathApplied;

		public BluePrintHeroDeath(ScenarioController controller)
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

		private bool HasLiveHeroContext()
		{
			return false;
		}
	}
}
