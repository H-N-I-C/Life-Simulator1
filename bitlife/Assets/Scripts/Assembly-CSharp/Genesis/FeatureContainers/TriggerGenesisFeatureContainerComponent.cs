namespace Genesis.FeatureContainers
{
	public class TriggerGenesisFeatureContainerComponent
	{
		private static TriggerManager.ITriggerScenario _triggerScenario;

		private static bool SuppressTriggers__BackingField;

		public static bool SuppressTriggers
		{
			get
			{
				return SuppressTriggers__BackingField;
			}
			set
			{
				SuppressTriggers__BackingField = value;
			}
		}

		public static TriggerManager.ITriggerScenario GetTrigger()
		{
			return null;
		}
	}
}
