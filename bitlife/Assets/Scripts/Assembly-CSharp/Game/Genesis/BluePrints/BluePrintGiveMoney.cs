using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintGiveMoney : BluePrintNpcInteractionBase
	{
		public BluePrintGiveMoney(ScenarioController controller)
			: base(null)
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
	}
}
