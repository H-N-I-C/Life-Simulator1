using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintArgue : BluePrintNpcInteractionBase
	{
		private string _reason;

		public BluePrintArgue(ScenarioController controller)
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
