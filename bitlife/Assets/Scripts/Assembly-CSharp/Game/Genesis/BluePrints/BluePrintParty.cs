using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintParty : BluePrintNpcInteractionBase
	{
		public BluePrintParty(ScenarioController controller)
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

		private Scenario GetCasinoCelebrityScenario(SimCasinoCelebrity celebrity)
		{
			return null;
		}
	}
}
