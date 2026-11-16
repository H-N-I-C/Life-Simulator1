namespace Genesis.FeatureContainers
{
	public sealed class GenesisFeaturePropertyLookupResult
	{
		private readonly GenesisFeaturePropertyLookupStatus Status__BackingField;

		private readonly GenesisFeaturePropertyDefinition Definition__BackingField;

		private readonly string Diagnostic__BackingField;

		public GenesisFeaturePropertyLookupStatus Status => Status__BackingField;

		public GenesisFeaturePropertyDefinition Definition => Definition__BackingField;

		public string Diagnostic => Diagnostic__BackingField;

		public bool AllowsLegacyUnboundedWrite => false;

		public bool IsSchemaReady => false;

		internal GenesisFeaturePropertyLookupResult(GenesisFeaturePropertyLookupStatus status, GenesisFeaturePropertyDefinition definition, string diagnostic)
		{
		}
	}
}
