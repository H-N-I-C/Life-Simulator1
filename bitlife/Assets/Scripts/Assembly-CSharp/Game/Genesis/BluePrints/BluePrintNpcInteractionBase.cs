using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public abstract class BluePrintNpcInteractionBase : IBluePrint
	{
		protected readonly ScenarioController _controller;

		private BluePrintType BluePrintType__BackingField;

		public BluePrintType BluePrintType
		{
			get
			{
				return BluePrintType__BackingField;
			}
			set
			{
				BluePrintType__BackingField = value;
			}
		}

		protected BluePrintNpcInteractionBase(ScenarioController controller)
		{
		}

		public virtual void Setup(BluePrintData data)
		{
		}

		protected SimPerson GetTargetNpc()
		{
			return null;
		}

		protected bool IsAbandonedChild(SimPerson person)
		{
			return false;
		}

		public abstract bool ConditionSelected();

		public abstract Scenario GetNextScenario(string followupScenario);
	}
}
