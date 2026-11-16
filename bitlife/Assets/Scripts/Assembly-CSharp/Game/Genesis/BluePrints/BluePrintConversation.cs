using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintConversation : BluePrintNpcInteractionBase
	{
		private int _intValue;

		public BluePrintConversation(ScenarioController controller)
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
	}
}
