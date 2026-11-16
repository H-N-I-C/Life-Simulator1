using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintCompliment : BluePrintNpcInteractionBase
	{
		public BluePrintCompliment(ScenarioController controller)
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
