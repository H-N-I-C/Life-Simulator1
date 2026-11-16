using System.Collections.Generic;
using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintNpcAttackInBound : BluePrintNpcInteractionBase
	{
		private bool _retaliatory;

		private int _heroDeathOverride;

		private string _customAttackerName;

		private int _attackerGender;

		private List<string> _customTitles;

		private string _customAttackerDescription;

		private string _customNarrative;

		private string _customExteriorTitle;

		public BluePrintNpcAttackInBound(ScenarioController controller)
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
