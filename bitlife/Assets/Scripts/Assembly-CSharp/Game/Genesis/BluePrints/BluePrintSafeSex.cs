using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintSafeSex : BluePrintNpcInteractionBase
	{
		private string _origin;

		public BluePrintSafeSex(ScenarioController controller)
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
