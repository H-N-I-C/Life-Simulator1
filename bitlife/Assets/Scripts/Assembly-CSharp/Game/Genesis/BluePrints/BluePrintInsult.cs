using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintInsult : BluePrintNpcInteractionBase
	{
		public BluePrintInsult(ScenarioController controller)
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
