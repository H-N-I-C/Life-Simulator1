using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintNpcAttacksHero : BluePrintNpcInteractionBase
	{
		private bool _canFightBack;

		private bool _canGetBusted;

		private int _killOverride;

		private string _genericAttackTarget;

		private int _genericTargetGender;

		public BluePrintNpcAttacksHero(ScenarioController controller)
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
