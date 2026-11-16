using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintSpendTime : BluePrintNpcInteractionBase
	{
		public BluePrintSpendTime(ScenarioController controller)
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
