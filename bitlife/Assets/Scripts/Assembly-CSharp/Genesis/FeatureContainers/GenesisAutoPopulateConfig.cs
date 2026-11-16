namespace Genesis.FeatureContainers
{
	public struct GenesisAutoPopulateConfig
	{
		public readonly string EntityType;

		public readonly int Target;

		public readonly string AssignedFlag;

		public bool IsValid => false;

		public GenesisAutoPopulateConfig(string entityType, int target, string assignedFlag)
		{
			EntityType = null;
			Target = 0;
			AssignedFlag = null;
		}
	}
}
