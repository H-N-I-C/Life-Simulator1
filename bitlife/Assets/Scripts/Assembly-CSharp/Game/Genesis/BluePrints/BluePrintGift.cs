using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintGift : BluePrintNpcInteractionBase
	{
		public BluePrintGift(ScenarioController controller)
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
