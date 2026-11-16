namespace Genesis.FeatureContainers
{
	public sealed class GenesisFeatureContainerSchemaApplyResult
	{
		private readonly GenesisFeatureContainerSchemaApplyStatus Status__BackingField;

		private readonly int NormalizedValueCount__BackingField;

		private readonly string Diagnostic__BackingField;

		public GenesisFeatureContainerSchemaApplyStatus Status => Status__BackingField;

		public int NormalizedValueCount => NormalizedValueCount__BackingField;

		public string Diagnostic => Diagnostic__BackingField;

		public bool Succeeded => false;

		public bool Retryable => false;

		private GenesisFeatureContainerSchemaApplyResult(GenesisFeatureContainerSchemaApplyStatus status, int normalizedValueCount, string diagnostic)
		{
		}

		public static GenesisFeatureContainerSchemaApplyResult Success(int normalizedValueCount)
		{
			return null;
		}

		public static GenesisFeatureContainerSchemaApplyResult Unavailable(string diagnostic, int normalizedValueCount = 0)
		{
			return null;
		}

		public static GenesisFeatureContainerSchemaApplyResult Invalid(string diagnostic, int normalizedValueCount = 0)
		{
			return null;
		}
	}
}
